using Claimly.APIs.Common;
using Claimly.APIs.Dtos;

namespace Claimly.APIs;

public interface IPoliciesService
{
    /// <summary>
    /// Create one Policy
    /// </summary>
    public Task<Policy> CreatePolicy(PolicyCreateInput policy);

    /// <summary>
    /// Delete one Policy
    /// </summary>
    public Task DeletePolicy(PolicyWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Policies
    /// </summary>
    public Task<List<Policy>> Policies(PolicyFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Policy records
    /// </summary>
    public Task<MetadataDto> PoliciesMeta(PolicyFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Policy
    /// </summary>
    public Task<Policy> Policy(PolicyWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Policy
    /// </summary>
    public Task UpdatePolicy(PolicyWhereUniqueInput uniqueId, PolicyUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Claims records to Policy
    /// </summary>
    public Task ConnectClaims(PolicyWhereUniqueInput uniqueId, ClaimWhereUniqueInput[] claimsId);

    /// <summary>
    /// Disconnect multiple Claims records from Policy
    /// </summary>
    public Task DisconnectClaims(PolicyWhereUniqueInput uniqueId, ClaimWhereUniqueInput[] claimsId);

    /// <summary>
    /// Find multiple Claims records for Policy
    /// </summary>
    public Task<List<Claim>> FindClaims(
        PolicyWhereUniqueInput uniqueId,
        ClaimFindManyArgs ClaimFindManyArgs
    );

    /// <summary>
    /// Update multiple Claims records for Policy
    /// </summary>
    public Task UpdateClaims(PolicyWhereUniqueInput uniqueId, ClaimWhereUniqueInput[] claimsId);

    /// <summary>
    /// Get a Customer record for Policy
    /// </summary>
    public Task<Customer> GetCustomer(PolicyWhereUniqueInput uniqueId);
}
