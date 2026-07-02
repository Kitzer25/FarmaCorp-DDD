using System.Security.Claims;
using API.Contracts.Cart;
using Application.UseCases.CartUseCase.Commands;
using Application.UseCases.CartUseCase.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commons;
using Domain.DTO_s.Cart;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ClientAccess)]
[Route("api/v1/customers/{customerId:int}/cart")]
public sealed class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveCart(int customerId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GETActiveCartQuery{ CustomerId = customerId } , ct);
        return Ok(result);
    }

    [HttpPost("items")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddItem(int customerId, [FromBody] AddItemRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new POSTAddCartItemCommand
        {
            CustomerId = customerId,
            ProductVariantId = request.ProductVariantId,
            Quantity = request.Quantity
        }, ct);
        return Ok(result);
    }

    [HttpPut("items/{cartItemId:int}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateItemQuantity(int customerId, int cartItemId, [FromBody] UpdateQuantityRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new PUTUpdateCartItemQuantityCommand
        {
            CustomerId = customerId,
            CartItemId = cartItemId,
            Quantity = request.Quantity
        }, ct);
        return Ok(result);
    }

    [HttpDelete("items/{cartItemId:int}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveItem(int customerId, int cartItemId, CancellationToken ct)
    {
        var result = await _mediator.Send(new DELETERemoveCartItemCommand
        {
            CustomerId = customerId,
            CartItemId = cartItemId
        }, ct);
        return Ok(result);
    }
}

public record AddItemRequest(int ProductVariantId, int Quantity);
public record UpdateQuantityRequest(int Quantity);
