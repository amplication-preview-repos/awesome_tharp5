using Claimly.APIs;
using Claimly.APIs.Common;
using Claimly.APIs.Dtos;
using Claimly.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Claimly.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ClaimsControllerBase : ControllerBase
{
    protected readonly IClaimsService _service;

    public ClaimsControllerBase(IClaimsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Claim
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Claim>> CreateClaim(ClaimCreateInput input)
    {
        var claim = await _service.CreateClaim(input);

        return CreatedAtAction(nameof(Claim), new { id = claim.Id }, claim);
    }

    /// <summary>
    /// Delete one Claim
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteClaim([FromRoute()] ClaimWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteClaim(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Claims
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Claim>>> Claims([FromQuery()] ClaimFindManyArgs filter)
    {
        return Ok(await _service.Claims(filter));
    }

    /// <summary>
    /// Meta data about Claim records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ClaimsMeta([FromQuery()] ClaimFindManyArgs filter)
    {
        return Ok(await _service.ClaimsMeta(filter));
    }

    /// <summary>
    /// Get one Claim
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Claim>> Claim([FromRoute()] ClaimWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Claim(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Claim
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateClaim(
        [FromRoute()] ClaimWhereUniqueInput uniqueId,
        [FromQuery()] ClaimUpdateInput claimUpdateDto
    )
    {
        try
        {
            await _service.UpdateClaim(uniqueId, claimUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a ClaimStatus record for Claim
    /// </summary>
    [HttpGet("{Id}/claimStatus")]
    public async Task<ActionResult<List<ClaimStatus>>> GetClaimStatus(
        [FromRoute()] ClaimWhereUniqueInput uniqueId
    )
    {
        var claimStatus = await _service.GetClaimStatus(uniqueId);
        return Ok(claimStatus);
    }

    /// <summary>
    /// Get a Customer record for Claim
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] ClaimWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Get a Policy record for Claim
    /// </summary>
    [HttpGet("{Id}/policy")]
    public async Task<ActionResult<List<Policy>>> GetPolicy(
        [FromRoute()] ClaimWhereUniqueInput uniqueId
    )
    {
        var policy = await _service.GetPolicy(uniqueId);
        return Ok(policy);
    }
}
