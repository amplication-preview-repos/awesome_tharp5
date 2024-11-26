using Claimly.APIs.Dtos;
using Claimly.Infrastructure.Models;

namespace Claimly.APIs.Extensions;

public static class ClaimsExtensions
{
    public static Claim ToDto(this ClaimDbModel model)
    {
        return new Claim
        {
            Amount = model.Amount,
            ClaimStatus = model.ClaimStatusId,
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Date = model.Date,
            Description = model.Description,
            Id = model.Id,
            Policy = model.PolicyId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ClaimDbModel ToModel(
        this ClaimUpdateInput updateDto,
        ClaimWhereUniqueInput uniqueId
    )
    {
        var claim = new ClaimDbModel
        {
            Id = uniqueId.Id,
            Amount = updateDto.Amount,
            Date = updateDto.Date,
            Description = updateDto.Description
        };

        if (updateDto.ClaimStatus != null)
        {
            claim.ClaimStatusId = updateDto.ClaimStatus;
        }
        if (updateDto.CreatedAt != null)
        {
            claim.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            claim.CustomerId = updateDto.Customer;
        }
        if (updateDto.Policy != null)
        {
            claim.PolicyId = updateDto.Policy;
        }
        if (updateDto.UpdatedAt != null)
        {
            claim.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return claim;
    }
}
