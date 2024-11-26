using Claimly.APIs;
using Claimly.APIs.Common;
using Claimly.APIs.Dtos;
using Claimly.APIs.Errors;
using Claimly.APIs.Extensions;
using Claimly.Infrastructure;
using Claimly.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Claimly.APIs;

public abstract class ClaimStatusesServiceBase : IClaimStatusesService
{
    protected readonly ClaimlyDbContext _context;

    public ClaimStatusesServiceBase(ClaimlyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one ClaimStatus
    /// </summary>
    public async Task<ClaimStatus> CreateClaimStatus(ClaimStatusCreateInput createDto)
    {
        var claimStatus = new ClaimStatusDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            claimStatus.Id = createDto.Id;
        }
        if (createDto.Claims != null)
        {
            claimStatus.Claims = await _context
                .Claims.Where(claim => createDto.Claims.Select(t => t.Id).Contains(claim.Id))
                .ToListAsync();
        }

        _context.ClaimStatuses.Add(claimStatus);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ClaimStatusDbModel>(claimStatus.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one ClaimStatus
    /// </summary>
    public async Task DeleteClaimStatus(ClaimStatusWhereUniqueInput uniqueId)
    {
        var claimStatus = await _context.ClaimStatuses.FindAsync(uniqueId.Id);
        if (claimStatus == null)
        {
            throw new NotFoundException();
        }

        _context.ClaimStatuses.Remove(claimStatus);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many ClaimStatuses
    /// </summary>
    public async Task<List<ClaimStatus>> ClaimStatuses(ClaimStatusFindManyArgs findManyArgs)
    {
        var claimStatuses = await _context
            .ClaimStatuses.Include(x => x.Claims)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return claimStatuses.ConvertAll(claimStatus => claimStatus.ToDto());
    }

    /// <summary>
    /// Meta data about ClaimStatus records
    /// </summary>
    public async Task<MetadataDto> ClaimStatusesMeta(ClaimStatusFindManyArgs findManyArgs)
    {
        var count = await _context.ClaimStatuses.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one ClaimStatus
    /// </summary>
    public async Task<ClaimStatus> ClaimStatus(ClaimStatusWhereUniqueInput uniqueId)
    {
        var claimStatuses = await this.ClaimStatuses(
            new ClaimStatusFindManyArgs { Where = new ClaimStatusWhereInput { Id = uniqueId.Id } }
        );
        var claimStatus = claimStatuses.FirstOrDefault();
        if (claimStatus == null)
        {
            throw new NotFoundException();
        }

        return claimStatus;
    }

    /// <summary>
    /// Update one ClaimStatus
    /// </summary>
    public async Task UpdateClaimStatus(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimStatusUpdateInput updateDto
    )
    {
        var claimStatus = updateDto.ToModel(uniqueId);

        if (updateDto.Claims != null)
        {
            claimStatus.Claims = await _context
                .Claims.Where(claim => updateDto.Claims.Select(t => t).Contains(claim.Id))
                .ToListAsync();
        }

        _context.Entry(claimStatus).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ClaimStatuses.Any(e => e.Id == claimStatus.Id))
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
    /// Connect multiple Claims records to ClaimStatus
    /// </summary>
    public async Task ConnectClaims(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .ClaimStatuses.Include(x => x.Claims)
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
    /// Disconnect multiple Claims records from ClaimStatus
    /// </summary>
    public async Task DisconnectClaims(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .ClaimStatuses.Include(x => x.Claims)
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
    /// Find multiple Claims records for ClaimStatus
    /// </summary>
    public async Task<List<Claim>> FindClaims(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimFindManyArgs claimStatusFindManyArgs
    )
    {
        var claims = await _context
            .Claims.Where(m => m.ClaimStatusId == uniqueId.Id)
            .ApplyWhere(claimStatusFindManyArgs.Where)
            .ApplySkip(claimStatusFindManyArgs.Skip)
            .ApplyTake(claimStatusFindManyArgs.Take)
            .ApplyOrderBy(claimStatusFindManyArgs.SortBy)
            .ToListAsync();

        return claims.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Claims records for ClaimStatus
    /// </summary>
    public async Task UpdateClaims(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] childrenIds
    )
    {
        var claimStatus = await _context
            .ClaimStatuses.Include(t => t.Claims)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (claimStatus == null)
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

        claimStatus.Claims = children;
        await _context.SaveChangesAsync();
    }
}
