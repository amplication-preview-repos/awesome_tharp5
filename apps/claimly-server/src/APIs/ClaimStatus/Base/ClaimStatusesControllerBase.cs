using Claimly.APIs;
using Claimly.APIs.Common;
using Claimly.APIs.Dtos;
using Claimly.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Claimly.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ClaimStatusesControllerBase : ControllerBase
{
    protected readonly IClaimStatusesService _service;

    public ClaimStatusesControllerBase(IClaimStatusesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one ClaimStatus
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<ClaimStatus>> CreateClaimStatus(ClaimStatusCreateInput input)
    {
        var claimStatus = await _service.CreateClaimStatus(input);

        return CreatedAtAction(nameof(ClaimStatus), new { id = claimStatus.Id }, claimStatus);
    }

    /// <summary>
    /// Delete one ClaimStatus
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteClaimStatus(
        [FromRoute()] ClaimStatusWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteClaimStatus(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many ClaimStatuses
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<ClaimStatus>>> ClaimStatuses(
        [FromQuery()] ClaimStatusFindManyArgs filter
    )
    {
        return Ok(await _service.ClaimStatuses(filter));
    }

    /// <summary>
    /// Meta data about ClaimStatus records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ClaimStatusesMeta(
        [FromQuery()] ClaimStatusFindManyArgs filter
    )
    {
        return Ok(await _service.ClaimStatusesMeta(filter));
    }

    /// <summary>
    /// Get one ClaimStatus
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<ClaimStatus>> ClaimStatus(
        [FromRoute()] ClaimStatusWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.ClaimStatus(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one ClaimStatus
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateClaimStatus(
        [FromRoute()] ClaimStatusWhereUniqueInput uniqueId,
        [FromQuery()] ClaimStatusUpdateInput claimStatusUpdateDto
    )
    {
        try
        {
            await _service.UpdateClaimStatus(uniqueId, claimStatusUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Claims records to ClaimStatus
    /// </summary>
    [HttpPost("{Id}/claims")]
    public async Task<ActionResult> ConnectClaims(
        [FromRoute()] ClaimStatusWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Claims records from ClaimStatus
    /// </summary>
    [HttpDelete("{Id}/claims")]
    public async Task<ActionResult> DisconnectClaims(
        [FromRoute()] ClaimStatusWhereUniqueInput uniqueId,
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
    /// Find multiple Claims records for ClaimStatus
    /// </summary>
    [HttpGet("{Id}/claims")]
    public async Task<ActionResult<List<Claim>>> FindClaims(
        [FromRoute()] ClaimStatusWhereUniqueInput uniqueId,
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
    /// Update multiple Claims records for ClaimStatus
    /// </summary>
    [HttpPatch("{Id}/claims")]
    public async Task<ActionResult> UpdateClaims(
        [FromRoute()] ClaimStatusWhereUniqueInput uniqueId,
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
}
