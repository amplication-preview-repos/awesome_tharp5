using Claimly.APIs;

namespace Claimly;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsService, ClaimsService>();
        services.AddScoped<IClaimStatusesService, ClaimStatusesService>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IPoliciesService, PoliciesService>();
    }
}
