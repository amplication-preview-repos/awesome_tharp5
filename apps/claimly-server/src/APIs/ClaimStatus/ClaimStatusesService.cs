using Claimly.Infrastructure;

namespace Claimly.APIs;

public class ClaimStatusesService : ClaimStatusesServiceBase
{
    public ClaimStatusesService(ClaimlyDbContext context)
        : base(context) { }
}
