using Claimly.APIs;
using Claimly.APIs.Common;
using Claimly.APIs.Dtos;
using Claimly.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Claimly.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Claims records to Customer
    /// </summary>
    [HttpPost("{Id}/claims")]
    public async Task<ActionResult> ConnectClaims(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Claims records from Customer
    /// </summary>
    [HttpDelete("{Id}/claims")]
    public async Task<ActionResult> DisconnectClaims(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
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
    /// Find multiple Claims records for Customer
    /// </summary>
    [HttpGet("{Id}/claims")]
    public async Task<ActionResult<List<Claim>>> FindClaims(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
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
    /// Update multiple Claims records for Customer
    /// </summary>
    [HttpPatch("{Id}/claims")]
    public async Task<ActionResult> UpdateClaims(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
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
    /// Connect multiple Policies records to Customer
    /// </summary>
    [HttpPost("{Id}/policies")]
    public async Task<ActionResult> ConnectPolicies(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] PolicyWhereUniqueInput[] policiesId
    )
    {
        try
        {
            await _service.ConnectPolicies(uniqueId, policiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Policies records from Customer
    /// </summary>
    [HttpDelete("{Id}/policies")]
    public async Task<ActionResult> DisconnectPolicies(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] PolicyWhereUniqueInput[] policiesId
    )
    {
        try
        {
            await _service.DisconnectPolicies(uniqueId, policiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Policies records for Customer
    /// </summary>
    [HttpGet("{Id}/policies")]
    public async Task<ActionResult<List<Policy>>> FindPolicies(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] PolicyFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindPolicies(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Policies records for Customer
    /// </summary>
    [HttpPatch("{Id}/policies")]
    public async Task<ActionResult> UpdatePolicies(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] PolicyWhereUniqueInput[] policiesId
    )
    {
        try
        {
            await _service.UpdatePolicies(uniqueId, policiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
