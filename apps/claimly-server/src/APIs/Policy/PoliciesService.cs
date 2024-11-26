using Claimly.Infrastructure;

namespace Claimly.APIs;

public class PoliciesService : PoliciesServiceBase
{
    public PoliciesService(ClaimlyDbContext context)
        : base(context) { }
}
