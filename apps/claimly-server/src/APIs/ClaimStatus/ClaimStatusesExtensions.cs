using Claimly.APIs.Dtos;
using Claimly.Infrastructure.Models;

namespace Claimly.APIs.Extensions;

public static class ClaimStatusesExtensions
{
    public static ClaimStatus ToDto(this ClaimStatusDbModel model)
    {
        return new ClaimStatus
        {
            Claims = model.Claims?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ClaimStatusDbModel ToModel(
        this ClaimStatusUpdateInput updateDto,
        ClaimStatusWhereUniqueInput uniqueId
    )
    {
        var claimStatus = new ClaimStatusDbModel { Id = uniqueId.Id, Status = updateDto.Status };

        if (updateDto.CreatedAt != null)
        {
            claimStatus.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            claimStatus.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return claimStatus;
    }
}
