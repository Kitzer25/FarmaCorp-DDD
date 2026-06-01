using System.Security.Claims;
using Application.Cart.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = "ClientAccess")]
[Route("api/v1/cart")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("items/{cartItemId:int}")]
    public async Task<IActionResult> RemoveItem(int cartItemId, CancellationToken ct)
    {
        var customerId = GetCustomerId();

        try
        {
            var result = await _mediator.Send(new RemoveCartItemCommand
            {
                CustomerId = customerId,
                CartItemId = cartItemId
            }, ct);

            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
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
}
