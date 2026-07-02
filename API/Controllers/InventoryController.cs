using Application.UseCases.InventoryUseCase.Queries;
using Domain.DTO_s.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/inventory")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("variants/{productVariantId:int}/stock")]
    [ProducesResponseType(typeof(StockAvailabilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAvailableStock(int productVariantId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAvailableStockQuery { ProductVariantId = productVariantId }, ct);
        return Ok(result);
    }
}
