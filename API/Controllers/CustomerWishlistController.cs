using Application.UseCases.CustomerWishlistUseCase.Commands;
using Application.UseCases.CustomerWishlistUseCase.Queries;
using Domain.Commons;
using Domain.DTO_s.CustomerWhislist;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ClientAccess)]
[Route("api/v1/customers/{customerId:int}/wishlist")]
public sealed class CustomerWishlistController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerWishlistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerWishlistDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWishlist(int customerId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GETCustomerWishlistQuery { CustomerId = customerId }, ct);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerWishlistDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddItem(int customerId, [FromBody] AddWishlistItemRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new POSTAddCustomerWishlistCommand
        {
            CustomerId = customerId,
            ProductVariantId = request.ProductVariantId
        }, ct);
        return CreatedAtAction(nameof(GetWishlist), new { customerId }, result);
    }

    [HttpDelete("{productVariantId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveItem(int customerId, int productVariantId, CancellationToken ct)
    {
        var removed = await _mediator.Send(new DELETERemoveCustomerWishlistCommand
        {
            CustomerId = customerId,
            ProductVariantId = productVariantId
        }, ct);

        return removed ? NoContent() : NotFound();
    }
}

public record AddWishlistItemRequest(int ProductVariantId);
