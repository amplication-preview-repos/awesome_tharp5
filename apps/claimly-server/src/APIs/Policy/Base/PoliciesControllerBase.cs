using Claimly.APIs;
using Claimly.APIs.Common;
using Claimly.APIs.Dtos;
using Claimly.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Claimly.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PoliciesControllerBase : ControllerBase
{
    protected readonly IPoliciesService _service;

    public PoliciesControllerBase(IPoliciesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Policy
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Policy>> CreatePolicy(PolicyCreateInput input)
    {
        var policy = await _service.CreatePolicy(input);

        return CreatedAtAction(nameof(Policy), new { id = policy.Id }, policy);
    }

    /// <summary>
    /// Delete one Policy
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeletePolicy([FromRoute()] PolicyWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePolicy(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Policies
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Policy>>> Policies([FromQuery()] PolicyFindManyArgs filter)
    {
        return Ok(await _service.Policies(filter));
    }

    /// <summary>
    /// Meta data about Policy records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PoliciesMeta(
        [FromQuery()] PolicyFindManyArgs filter
    )
    {
        return Ok(await _service.PoliciesMeta(filter));
    }

    /// <summary>
    /// Get one Policy
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Policy>> Policy([FromRoute()] PolicyWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Policy(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Policy
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdatePolicy(
        [FromRoute()] PolicyWhereUniqueInput uniqueId,
        [FromQuery()] PolicyUpdateInput policyUpdateDto
    )
    {
        try
        {
            await _service.UpdatePolicy(uniqueId, policyUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Claims records to Policy
    /// </summary>
    [HttpPost("{Id}/claims")]
    public async Task<ActionResult> ConnectClaims(
        [FromRoute()] PolicyWhereUniqueInput uniqueId,
        [FromQuery()] ClaimWhereUniqueInput[] claimsId
    )
    {
        try
        {
            await _service.ConnectClaims(uniqueId, claimsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Claims records from Policy
    /// </summary>
    [HttpDelete("{Id}/claims")]
    public async Task<ActionResult> DisconnectClaims(
        [FromRoute()] PolicyWhereUniqueInput uniqueId,
        [FromBody()] ClaimWhereUniqueInput[] claimsId
    )
    {
        try
        {
            await _service.DisconnectClaims(uniqueId, claimsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Claims records for Policy
    /// </summary>
    [HttpGet("{Id}/claims")]
    public async Task<ActionResult<List<Claim>>> FindClaims(
        [FromRoute()] PolicyWhereUniqueInput uniqueId,
        [FromQuery()] ClaimFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindClaims(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Claims records for Policy
    /// </summary>
    [HttpPatch("{Id}/claims")]
    public async Task<ActionResult> UpdateClaims(
        [FromRoute()] PolicyWhereUniqueInput uniqueId,
        [FromBody()] ClaimWhereUniqueInput[] claimsId
    )
    {
        try
        {
            await _service.UpdateClaims(uniqueId, claimsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Policy
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] PolicyWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
