using System.Security.Claims;
using API.Contracts.CustomerAddresses;
using Application.UseCases.CustomerAddressUseCase.Commands;
using Application.UseCases.CustomerAddressUseCase.Querys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commons;
using Domain.DTO_s.CustomerAddresses;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ClientAccess)]
[Route("api/v1/customer/addresses")]
public class CustomerAddressesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerAddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerAddressDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyAddresses(CancellationToken ct)
    {
        var customerId = GetCustomerId();
        var addresses = await _mediator.Send(new GetCustomerAddressesQuery { CustomerId = customerId }, ct);

        return Ok(addresses);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerAddressDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CustomerAddressRequest request, CancellationToken ct)
    {
        var validationError = ValidateRequest(request);

        if (validationError != null)
        {
            return BadRequest(new { message = validationError });
        }

        var customerId = GetCustomerId();
        var result = await _mediator.Send(new CreateCustomerAddressCommand
        {
            CustomerId = customerId,
            Label = request.Label,
            RecipientName = request.RecipientName,
            Street = request.Street,
            District = request.District,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            Phone = request.Phone,
            IsDefault = request.IsDefault
        }, ct);

        return CreatedAtAction(nameof(GetMyAddresses), new { id = result.AddressId }, result);
    }

    [HttpPut("{addressId:int}")]
    [ProducesResponseType(typeof(CustomerAddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int addressId, CustomerAddressRequest request, CancellationToken ct)
    {
        var validationError = ValidateRequest(request);

        if (validationError != null)
        {
            return BadRequest(new { message = validationError });
        }

        var customerId = GetCustomerId();
        var result = await _mediator.Send(new UpdateCustomerAddressCommand
        {
            CustomerId = customerId,
            AddressId = addressId,
            Label = request.Label,
            RecipientName = request.RecipientName,
            Street = request.Street,
            District = request.District,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            Phone = request.Phone,
            IsDefault = request.IsDefault
        }, ct);

        return Ok(result);
    }

    [HttpPatch("{addressId:int}/default")]
    [ProducesResponseType(typeof(CustomerAddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetDefault(int addressId, CancellationToken ct)
    {
        var customerId = GetCustomerId();
        var result = await _mediator.Send(new SetDefaultCustomerAddressCommand
        {
            CustomerId = customerId,
            AddressId = addressId
        }, ct);

        return Ok(result);
    }

    [HttpDelete("{addressId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int addressId, CancellationToken ct)
    {
        var customerId = GetCustomerId();
        await _mediator.Send(new DeleteCustomerAddressCommand
        {
            CustomerId = customerId,
            AddressId = addressId
        }, ct);

        return NoContent();
    }

    private int GetCustomerId()
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(claimValue, out var customerId))
        {
            throw new UnauthorizedAccessException("Token de cliente inválido.");
        }

        return customerId;
    }

    private static string? ValidateRequest(CustomerAddressRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Street))
        {
            return "La calle es obligatoria.";
        }

        if (string.IsNullOrWhiteSpace(request.City))
        {
            return "La ciudad es obligatoria.";
        }

        return null;
    }
}
