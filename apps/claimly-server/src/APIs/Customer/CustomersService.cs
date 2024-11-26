using Claimly.Infrastructure;

namespace Claimly.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(ClaimlyDbContext context)
        : base(context) { }
}
