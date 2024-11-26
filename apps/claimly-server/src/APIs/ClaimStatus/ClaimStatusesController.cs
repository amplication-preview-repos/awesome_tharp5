using Microsoft.AspNetCore.Mvc;

namespace Claimly.APIs;

[ApiController()]
public class ClaimStatusesController : ClaimStatusesControllerBase
{
    public ClaimStatusesController(IClaimStatusesService service)
        : base(service) { }
}
