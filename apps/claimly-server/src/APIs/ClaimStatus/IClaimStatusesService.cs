using Claimly.APIs.Common;
using Claimly.APIs.Dtos;

namespace Claimly.APIs;

public interface IClaimStatusesService
{
    /// <summary>
    /// Create one ClaimStatus
    /// </summary>
    public Task<ClaimStatus> CreateClaimStatus(ClaimStatusCreateInput claimstatus);

    /// <summary>
    /// Delete one ClaimStatus
    /// </summary>
    public Task DeleteClaimStatus(ClaimStatusWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many ClaimStatuses
    /// </summary>
    public Task<List<ClaimStatus>> ClaimStatuses(ClaimStatusFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about ClaimStatus records
    /// </summary>
    public Task<MetadataDto> ClaimStatusesMeta(ClaimStatusFindManyArgs findManyArgs);

    /// <summary>
    /// Get one ClaimStatus
    /// </summary>
    public Task<ClaimStatus> ClaimStatus(ClaimStatusWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one ClaimStatus
    /// </summary>
    public Task UpdateClaimStatus(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimStatusUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Claims records to ClaimStatus
    /// </summary>
    public Task ConnectClaims(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] claimsId
    );

    /// <summary>
    /// Disconnect multiple Claims records from ClaimStatus
    /// </summary>
    public Task DisconnectClaims(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] claimsId
    );

    /// <summary>
    /// Find multiple Claims records for ClaimStatus
    /// </summary>
    public Task<List<Claim>> FindClaims(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimFindManyArgs ClaimFindManyArgs
    );

    /// <summary>
    /// Update multiple Claims records for ClaimStatus
    /// </summary>
    public Task UpdateClaims(
        ClaimStatusWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] claimsId
    );
}
