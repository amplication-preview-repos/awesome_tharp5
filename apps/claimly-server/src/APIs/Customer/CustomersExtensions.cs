using Claimly.APIs.Dtos;
using Claimly.Infrastructure.Models;

namespace Claimly.APIs.Extensions;

public static class CustomersExtensions
{
    public static Customer ToDto(this CustomerDbModel model)
    {
        return new Customer
        {
            Claims = model.Claims?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            Id = model.Id,
            Name = model.Name,
            Phone = model.Phone,
            Policies = model.Policies?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CustomerDbModel ToModel(
        this CustomerUpdateInput updateDto,
        CustomerWhereUniqueInput uniqueId
    )
    {
        var customer = new CustomerDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            Name = updateDto.Name,
            Phone = updateDto.Phone
        };

        if (updateDto.CreatedAt != null)
        {
            customer.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            customer.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return customer;
    }
}
