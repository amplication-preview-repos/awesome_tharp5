using Claimly.APIs.Dtos;
using Claimly.Infrastructure.Models;

namespace Claimly.APIs.Extensions;

public static class PoliciesExtensions
{
    public static Policy ToDto(this PolicyDbModel model)
    {
        return new Policy
        {
            Claims = model.Claims?.Select(x => x.Id).ToList(),
            CoverageAmount = model.CoverageAmount,
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            PolicyNumber = model.PolicyNumber,
            Premium = model.Premium,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PolicyDbModel ToModel(
        this PolicyUpdateInput updateDto,
        PolicyWhereUniqueInput uniqueId
    )
    {
        var policy = new PolicyDbModel
        {
            Id = uniqueId.Id,
            CoverageAmount = updateDto.CoverageAmount,
            PolicyNumber = updateDto.PolicyNumber,
            Premium = updateDto.Premium
        };

        if (updateDto.CreatedAt != null)
        {
            policy.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            policy.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            policy.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return policy;
    }
}
