using System.Security.Claims;
using Core.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.DTO_s.Orders;
using Core.Ports.Services;
using Core.Ports.Services.EServices;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ClientAccess)]
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(CheckoutRequestDto request, CancellationToken ct)
    {
        if (request.ShippingAddressId <= 0)
        {
            return BadRequest(new { message = "La dirección de envío es obligatoria." });
        }

        if (request.PaymentMethodId <= 0)
        {
            return BadRequest(new { message = "El método de pago es obligatorio." });
        }

        try
        {
            var result = await _orderService.CheckoutAsync(GetUserId(), request, ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private int GetUserId()
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(claimValue, out var userId))
        {
            throw new UnauthorizedAccessException("Token inválido.");
        }

        return userId;
    }
}
