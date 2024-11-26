using Microsoft.AspNetCore.Mvc;

namespace Claimly.APIs;

[ApiController()]
public class ClaimsController : ClaimsControllerBase
{
    public ClaimsController(IClaimsService service)
        : base(service) { }
}
