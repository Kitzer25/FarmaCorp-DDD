using System.Security.Claims;
using API.Contracts.Cart;
using Application.UseCases.Cart.Commands;
using Application.UseCases.Cart.Querys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/customers/{customerId:int}/cart")]
public sealed class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveCart(int customerId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetActiveCartQuery{ CustomerId = customerId } , ct);
        return Ok(result);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem(int customerId, [FromBody] AddItemRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new AddCartItemCommand 
        { 
            CustomerId = customerId, 
            ProductVariantId = request.ProductVariantId, 
            Quantity = request.Quantity 
        }, ct);
        return Ok(result);
    }

    [HttpPut("items/{cartItemId:int}")]
    public async Task<IActionResult> UpdateItemQuantity(int customerId, int cartItemId, [FromBody] UpdateQuantityRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateCartItemQuantityCommand 
        { 
            CustomerId = customerId, 
            CartItemId = cartItemId, 
            Quantity = request.Quantity 
        }, ct);
        return Ok(result);
    }
}

public record AddItemRequest(int ProductVariantId, int Quantity);
public record UpdateQuantityRequest(int Quantity);
