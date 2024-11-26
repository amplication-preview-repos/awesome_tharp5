using Claimly.Infrastructure;

namespace Claimly.APIs;

public class ClaimsService : ClaimsServiceBase
{
    public ClaimsService(ClaimlyDbContext context)
        : base(context) { }
}
