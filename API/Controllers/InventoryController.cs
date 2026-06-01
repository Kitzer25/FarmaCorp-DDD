using Application.Inventory.Dtos;
using Core.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/inventory")]
public class InventoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public InventoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("variants/{productVariantId:int}/stock")]
    public async Task<IActionResult> GetAvailableStock(int productVariantId, CancellationToken ct)
    {
        var inventory = await _unitOfWork.InventoryRepo.GetByProductVariantIdAsync(productVariantId, ct);

        if (inventory == null)
        {
            return NotFound(new { message = "No existe stock registrado para la variante indicada." });
        }

        var availableQuantity = Math.Max(0, inventory.quantity_on_hand - inventory.reserved_quantity);

        return Ok(new StockAvailabilityDto
        {
            ProductVariantId = inventory.product_variant_id,
            QuantityOnHand = inventory.quantity_on_hand,
            ReservedQuantity = inventory.reserved_quantity,
            AvailableQuantity = availableQuantity,
            MinStockLevel = inventory.min_stock_level,
            IsLowStock = availableQuantity <= inventory.min_stock_level
        });
    }
}
