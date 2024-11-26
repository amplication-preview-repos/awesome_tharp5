using Claimly.APIs;
using Claimly.APIs.Common;
using Claimly.APIs.Dtos;
using Claimly.APIs.Errors;
using Claimly.APIs.Extensions;
using Claimly.Infrastructure;
using Claimly.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Claimly.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly ClaimlyDbContext _context;

    public CustomersServiceBase(ClaimlyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<Customer> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new CustomerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Name = createDto.Name,
            Phone = createDto.Phone,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Claims != null)
        {
            customer.Claims = await _context
                .Claims.Where(claim => createDto.Claims.Select(t => t.Id).Contains(claim.Id))
                .ToListAsync();
        }

        if (createDto.Policies != null)
        {
            customer.Policies = await _context
                .Policies.Where(policy => createDto.Policies.Select(t => t.Id).Contains(policy.Id))
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CustomerDbModel>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerWhereUniqueInput uniqueId)
    {
        var customer = await _context.Customers.FindAsync(uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.Claims)
            .Include(x => x.Policies)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public async Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs)
    {
        var count = await _context.Customers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<Customer> Customer(CustomerWhereUniqueInput uniqueId)
    {
        var customers = await this.Customers(
            new CustomerFindManyArgs { Where = new CustomerWhereInput { Id = uniqueId.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(
        CustomerWhereUniqueInput uniqueId,
        CustomerUpdateInput updateDto
    )
    {
        var customer = updateDto.ToModel(uniqueId);

        if (updateDto.Claims != null)
        {
            customer.Claims = await _context
                .Claims.Where(claim => updateDto.Claims.Select(t => t).Contains(claim.Id))
                .ToListAsync();
        }

        if (updateDto.Policies != null)
        {
            customer.Policies = await _context
                .Policies.Where(policy => updateDto.Policies.Select(t => t).Contains(policy.Id))
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Claims records to Customer
    /// </summary>
    public async Task ConnectClaims(
        CustomerWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Claims)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Claims.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Claims);

        foreach (var child in childrenToConnect)
        {
            parent.Claims.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Claims records from Customer
    /// </summary>
    public async Task DisconnectClaims(
        CustomerWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Claims)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Claims.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Claims?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Claims records for Customer
    /// </summary>
    public async Task<List<Claim>> FindClaims(
        CustomerWhereUniqueInput uniqueId,
        ClaimFindManyArgs customerFindManyArgs
    )
    {
        var claims = await _context
            .Claims.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return claims.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Claims records for Customer
    /// </summary>
    public async Task UpdateClaims(
        CustomerWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Claims)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Claims.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Claims = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Policies records to Customer
    /// </summary>
    public async Task ConnectPolicies(
        CustomerWhereUniqueInput uniqueId,
        PolicyWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Policies)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Policies.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Policies);

        foreach (var child in childrenToConnect)
        {
            parent.Policies.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Policies records from Customer
    /// </summary>
    public async Task DisconnectPolicies(
        CustomerWhereUniqueInput uniqueId,
        PolicyWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Policies)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Policies.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Policies?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Policies records for Customer
    /// </summary>
    public async Task<List<Policy>> FindPolicies(
        CustomerWhereUniqueInput uniqueId,
        PolicyFindManyArgs customerFindManyArgs
    )
    {
        var policies = await _context
            .Policies.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return policies.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Policies records for Customer
    /// </summary>
    public async Task UpdatePolicies(
        CustomerWhereUniqueInput uniqueId,
        PolicyWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Policies)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Policies.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Policies = children;
        await _context.SaveChangesAsync();
    }
}
