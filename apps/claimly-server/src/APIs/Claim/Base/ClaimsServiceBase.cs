using Claimly.APIs;
using Claimly.APIs.Common;
using Claimly.APIs.Dtos;
using Claimly.APIs.Errors;
using Claimly.APIs.Extensions;
using Claimly.Infrastructure;
using Claimly.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Claimly.APIs;

public abstract class ClaimsServiceBase : IClaimsService
{
    protected readonly ClaimlyDbContext _context;

    public ClaimsServiceBase(ClaimlyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Claim
    /// </summary>
    public async Task<Claim> CreateClaim(ClaimCreateInput createDto)
    {
        var claim = new ClaimDbModel
        {
            Amount = createDto.Amount,
            CreatedAt = createDto.CreatedAt,
            Date = createDto.Date,
            Description = createDto.Description,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            claim.Id = createDto.Id;
        }
        if (createDto.ClaimStatus != null)
        {
            claim.ClaimStatus = await _context
                .ClaimStatuses.Where(claimStatus => createDto.ClaimStatus.Id == claimStatus.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Customer != null)
        {
            claim.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Policy != null)
        {
            claim.Policy = await _context
                .Policies.Where(policy => createDto.Policy.Id == policy.Id)
                .FirstOrDefaultAsync();
        }

        _context.Claims.Add(claim);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ClaimDbModel>(claim.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Claim
    /// </summary>
    public async Task DeleteClaim(ClaimWhereUniqueInput uniqueId)
    {
        var claim = await _context.Claims.FindAsync(uniqueId.Id);
        if (claim == null)
        {
            throw new NotFoundException();
        }

        _context.Claims.Remove(claim);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Claims
    /// </summary>
    public async Task<List<Claim>> Claims(ClaimFindManyArgs findManyArgs)
    {
        var claims = await _context
            .Claims.Include(x => x.ClaimStatus)
            .Include(x => x.Customer)
            .Include(x => x.Policy)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return claims.ConvertAll(claim => claim.ToDto());
    }

    /// <summary>
    /// Meta data about Claim records
    /// </summary>
    public async Task<MetadataDto> ClaimsMeta(ClaimFindManyArgs findManyArgs)
    {
        var count = await _context.Claims.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Claim
    /// </summary>
    public async Task<Claim> Claim(ClaimWhereUniqueInput uniqueId)
    {
        var claims = await this.Claims(
            new ClaimFindManyArgs { Where = new ClaimWhereInput { Id = uniqueId.Id } }
        );
        var claim = claims.FirstOrDefault();
        if (claim == null)
        {
            throw new NotFoundException();
        }

        return claim;
    }

    /// <summary>
    /// Update one Claim
    /// </summary>
    public async Task UpdateClaim(ClaimWhereUniqueInput uniqueId, ClaimUpdateInput updateDto)
    {
        var claim = updateDto.ToModel(uniqueId);

        if (updateDto.ClaimStatus != null)
        {
            claim.ClaimStatus = await _context
                .ClaimStatuses.Where(claimStatus => updateDto.ClaimStatus == claimStatus.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Customer != null)
        {
            claim.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Policy != null)
        {
            claim.Policy = await _context
                .Policies.Where(policy => updateDto.Policy == policy.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(claim).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Claims.Any(e => e.Id == claim.Id))
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
    /// Get a ClaimStatus record for Claim
    /// </summary>
    public async Task<ClaimStatus> GetClaimStatus(ClaimWhereUniqueInput uniqueId)
    {
        var claim = await _context
            .Claims.Where(claim => claim.Id == uniqueId.Id)
            .Include(claim => claim.ClaimStatus)
            .FirstOrDefaultAsync();
        if (claim == null)
        {
            throw new NotFoundException();
        }
        return claim.ClaimStatus.ToDto();
    }

    /// <summary>
    /// Get a Customer record for Claim
    /// </summary>
    public async Task<Customer> GetCustomer(ClaimWhereUniqueInput uniqueId)
    {
        var claim = await _context
            .Claims.Where(claim => claim.Id == uniqueId.Id)
            .Include(claim => claim.Customer)
            .FirstOrDefaultAsync();
        if (claim == null)
        {
            throw new NotFoundException();
        }
        return claim.Customer.ToDto();
    }

    /// <summary>
    /// Get a Policy record for Claim
    /// </summary>
    public async Task<Policy> GetPolicy(ClaimWhereUniqueInput uniqueId)
    {
        var claim = await _context
            .Claims.Where(claim => claim.Id == uniqueId.Id)
            .Include(claim => claim.Policy)
            .FirstOrDefaultAsync();
        if (claim == null)
        {
            throw new NotFoundException();
        }
        return claim.Policy.ToDto();
    }
}
