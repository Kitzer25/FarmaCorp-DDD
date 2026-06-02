using System.Security.Claims;
using API.Contracts.Cart;
using Application.Cart.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Constants;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ClientAccess)]
[Route("api/v1/cart")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveCart(CancellationToken ct)
    {
        var customerId = GetCustomerId();
        var result = await _mediator.Send(new GetActiveCartCommand
        {
            CustomerId = customerId
        }, ct);

        return Ok(result);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem(AddCartItemRequest request, CancellationToken ct)
    {
        if (request.ProductVariantId <= 0)
        {
            return BadRequest(new { message = "La variante del producto es obligatoria." });
        }

        if (request.Quantity <= 0)
        {
            return BadRequest(new { message = "La cantidad debe ser mayor a 0." });
        }

        try
        {
            var customerId = GetCustomerId();
            var result = await _mediator.Send(new AddCartItemCommand
            {
                CustomerId = customerId,
                ProductVariantId = request.ProductVariantId,
                Quantity = request.Quantity
            }, ct);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("items/{cartItemId:int}")]
    public async Task<IActionResult> UpdateItemQuantity(
        int cartItemId,
        UpdateCartItemQuantityRequest request,
        CancellationToken ct)
    {
        if (request.Quantity <= 0)
        {
            return BadRequest(new { message = "La cantidad debe ser mayor a 0." });
        }

        try
        {
            var customerId = GetCustomerId();
            var result = await _mediator.Send(new UpdateCartItemQuantityCommand
            {
                CustomerId = customerId,
                CartItemId = cartItemId,
                Quantity = request.Quantity
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
