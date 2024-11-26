using Claimly.APIs;
using Claimly.APIs.Common;
using Claimly.APIs.Dtos;
using Claimly.APIs.Errors;
using Claimly.APIs.Extensions;
using Claimly.Infrastructure;
using Claimly.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Claimly.APIs;

public abstract class PoliciesServiceBase : IPoliciesService
{
    protected readonly ClaimlyDbContext _context;

    public PoliciesServiceBase(ClaimlyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Policy
    /// </summary>
    public async Task<Policy> CreatePolicy(PolicyCreateInput createDto)
    {
        var policy = new PolicyDbModel
        {
            CoverageAmount = createDto.CoverageAmount,
            CreatedAt = createDto.CreatedAt,
            PolicyNumber = createDto.PolicyNumber,
            Premium = createDto.Premium,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            policy.Id = createDto.Id;
        }
        if (createDto.Claims != null)
        {
            policy.Claims = await _context
                .Claims.Where(claim => createDto.Claims.Select(t => t.Id).Contains(claim.Id))
                .ToListAsync();
        }

        if (createDto.Customer != null)
        {
            policy.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Policies.Add(policy);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PolicyDbModel>(policy.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Policy
    /// </summary>
    public async Task DeletePolicy(PolicyWhereUniqueInput uniqueId)
    {
        var policy = await _context.Policies.FindAsync(uniqueId.Id);
        if (policy == null)
        {
            throw new NotFoundException();
        }

        _context.Policies.Remove(policy);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Policies
    /// </summary>
    public async Task<List<Policy>> Policies(PolicyFindManyArgs findManyArgs)
    {
        var policies = await _context
            .Policies.Include(x => x.Claims)
            .Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return policies.ConvertAll(policy => policy.ToDto());
    }

    /// <summary>
    /// Meta data about Policy records
    /// </summary>
    public async Task<MetadataDto> PoliciesMeta(PolicyFindManyArgs findManyArgs)
    {
        var count = await _context.Policies.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Policy
    /// </summary>
    public async Task<Policy> Policy(PolicyWhereUniqueInput uniqueId)
    {
        var policies = await this.Policies(
            new PolicyFindManyArgs { Where = new PolicyWhereInput { Id = uniqueId.Id } }
        );
        var policy = policies.FirstOrDefault();
        if (policy == null)
        {
            throw new NotFoundException();
        }

        return policy;
    }

    /// <summary>
    /// Update one Policy
    /// </summary>
    public async Task UpdatePolicy(PolicyWhereUniqueInput uniqueId, PolicyUpdateInput updateDto)
    {
        var policy = updateDto.ToModel(uniqueId);

        if (updateDto.Claims != null)
        {
            policy.Claims = await _context
                .Claims.Where(claim => updateDto.Claims.Select(t => t).Contains(claim.Id))
                .ToListAsync();
        }

        if (updateDto.Customer != null)
        {
            policy.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(policy).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Policies.Any(e => e.Id == policy.Id))
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
    /// Connect multiple Claims records to Policy
    /// </summary>
    public async Task ConnectClaims(
        PolicyWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Policies.Include(x => x.Claims)
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
    /// Disconnect multiple Claims records from Policy
    /// </summary>
    public async Task DisconnectClaims(
        PolicyWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Policies.Include(x => x.Claims)
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
    /// Find multiple Claims records for Policy
    /// </summary>
    public async Task<List<Claim>> FindClaims(
        PolicyWhereUniqueInput uniqueId,
        ClaimFindManyArgs policyFindManyArgs
    )
    {
        var claims = await _context
            .Claims.Where(m => m.PolicyId == uniqueId.Id)
            .ApplyWhere(policyFindManyArgs.Where)
            .ApplySkip(policyFindManyArgs.Skip)
            .ApplyTake(policyFindManyArgs.Take)
            .ApplyOrderBy(policyFindManyArgs.SortBy)
            .ToListAsync();

        return claims.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Claims records for Policy
    /// </summary>
    public async Task UpdateClaims(
        PolicyWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var policy = await _context
            .Policies.Include(t => t.Claims)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (policy == null)
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

        policy.Claims = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Customer record for Policy
    /// </summary>
    public async Task<Customer> GetCustomer(PolicyWhereUniqueInput uniqueId)
    {
        var policy = await _context
            .Policies.Where(policy => policy.Id == uniqueId.Id)
            .Include(policy => policy.Customer)
            .FirstOrDefaultAsync();
        if (policy == null)
        {
            throw new NotFoundException();
        }
        return policy.Customer.ToDto();
    }
}
