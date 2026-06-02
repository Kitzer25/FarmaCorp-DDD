using System.Security.Claims;
using API.Contracts.CustomerAddresses;
using Application.CustomerAddresses.Commands;
using Application.CustomerAddresses.Dtos;
using Core.Ports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Constants;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ClientAccess)]
[Route("api/v1/customer/addresses")]
public class CustomerAddressesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CustomerAddressesController(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyAddresses(CancellationToken ct)
    {
        var customerId = GetCustomerId();
        var addresses = await _unitOfWork.CustomerAddressRepo.GetActiveByCustomerIdAsync(customerId, ct);

        return Ok(addresses.Select(CustomerAddressMapper.ToDto));
    }

    [HttpPost]
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
    public async Task<IActionResult> Update(int addressId, CustomerAddressRequest request, CancellationToken ct)
    {
        var validationError = ValidateRequest(request);

        if (validationError != null)
        {
            return BadRequest(new { message = validationError });
        }

        try
        {
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
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPatch("{addressId:int}/default")]
    public async Task<IActionResult> SetDefault(int addressId, CancellationToken ct)
    {
        try
        {
            var customerId = GetCustomerId();
            var result = await _mediator.Send(new SetDefaultCustomerAddressCommand
            {
                CustomerId = customerId,
                AddressId = addressId
            }, ct);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{addressId:int}")]
    public async Task<IActionResult> Delete(int addressId, CancellationToken ct)
    {
        try
        {
            var customerId = GetCustomerId();
            await _mediator.Send(new DeleteCustomerAddressCommand
            {
                CustomerId = customerId,
                AddressId = addressId
            }, ct);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
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
