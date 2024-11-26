using Claimly.APIs.Common;
using Claimly.APIs.Dtos;

namespace Claimly.APIs;

public interface IClaimsService
{
    /// <summary>
    /// Create one Claim
    /// </summary>
    public Task<Claim> CreateClaim(ClaimCreateInput claim);

    /// <summary>
    /// Delete one Claim
    /// </summary>
    public Task DeleteClaim(ClaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Claims
    /// </summary>
    public Task<List<Claim>> Claims(ClaimFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Claim records
    /// </summary>
    public Task<MetadataDto> ClaimsMeta(ClaimFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Claim
    /// </summary>
    public Task<Claim> Claim(ClaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Claim
    /// </summary>
    public Task UpdateClaim(ClaimWhereUniqueInput uniqueId, ClaimUpdateInput updateDto);

    /// <summary>
    /// Get a ClaimStatus record for Claim
    /// </summary>
    public Task<ClaimStatus> GetClaimStatus(ClaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Customer record for Claim
    /// </summary>
    public Task<Customer> GetCustomer(ClaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Policy record for Claim
    /// </summary>
    public Task<Policy> GetPolicy(ClaimWhereUniqueInput uniqueId);
}
