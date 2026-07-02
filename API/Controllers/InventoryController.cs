using Application.UseCases.Inventory.Querys;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers;

[ApiController]
[Route("api/v1/inventory")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("variants/{productVariantId:int}/stock")]
    public async Task<IActionResult> GetAvailableStock(int productVariantId, CancellationToken ct)
    {
        try
        {
            var result = await _mediator.Send(new GetAvailableStockQuery { ProductVariantId = productVariantId }, ct);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
