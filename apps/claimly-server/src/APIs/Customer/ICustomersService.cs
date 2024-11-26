using Claimly.APIs.Common;
using Claimly.APIs.Dtos;

namespace Claimly.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Claims records to Customer
    /// </summary>
    public Task ConnectClaims(CustomerWhereUniqueInput uniqueId, ClaimWhereUniqueInput[] claimsId);

    /// <summary>
    /// Disconnect multiple Claims records from Customer
    /// </summary>
    public Task DisconnectClaims(
        CustomerWhereUniqueInput uniqueId,
        ClaimWhereUniqueInput[] claimsId
    );

    /// <summary>
    /// Find multiple Claims records for Customer
    /// </summary>
    public Task<List<Claim>> FindClaims(
        CustomerWhereUniqueInput uniqueId,
        ClaimFindManyArgs ClaimFindManyArgs
    );

    /// <summary>
    /// Update multiple Claims records for Customer
    /// </summary>
    public Task UpdateClaims(CustomerWhereUniqueInput uniqueId, ClaimWhereUniqueInput[] claimsId);

    /// <summary>
    /// Connect multiple Policies records to Customer
    /// </summary>
    public Task ConnectPolicies(
        CustomerWhereUniqueInput uniqueId,
        PolicyWhereUniqueInput[] policiesId
    );

    /// <summary>
    /// Disconnect multiple Policies records from Customer
    /// </summary>
    public Task DisconnectPolicies(
        CustomerWhereUniqueInput uniqueId,
        PolicyWhereUniqueInput[] policiesId
    );

    /// <summary>
    /// Find multiple Policies records for Customer
    /// </summary>
    public Task<List<Policy>> FindPolicies(
        CustomerWhereUniqueInput uniqueId,
        PolicyFindManyArgs PolicyFindManyArgs
    );

    /// <summary>
    /// Update multiple Policies records for Customer
    /// </summary>
    public Task UpdatePolicies(
        CustomerWhereUniqueInput uniqueId,
        PolicyWhereUniqueInput[] policiesId
    );
}
