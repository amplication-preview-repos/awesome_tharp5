using Microsoft.AspNetCore.Mvc;

namespace Claimly.APIs;

[ApiController()]
public class PoliciesController : PoliciesControllerBase
{
    public PoliciesController(IPoliciesService service)
        : base(service) { }
}
