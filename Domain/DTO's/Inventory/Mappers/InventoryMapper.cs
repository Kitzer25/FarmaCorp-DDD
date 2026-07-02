using Domain.Entities;

namespace Domain.DTO_s.Inventory.Mappers;

public static class InventoryMapper
{
    public static InventoryItemDto ToDto(this Domain.Entities.Inventory inventory)
    {
        var available = Math.Max(0, inventory.quantity_on_hand - inventory.reserved_quantity);

        return new InventoryItemDto
        {
            InventoryId = inventory.inventory_id,
            ProductVariantId = inventory.product_variant_id,
            ProductName = inventory.product_variant?.product?.name,
            Sku = inventory.product_variant?.sku,
            QuantityOnHand = inventory.quantity_on_hand,
            ReservedQuantity = inventory.reserved_quantity,
            AvailableQuantity = available,
            MinStockLevel = inventory.min_stock_level,
            IsLowStock = available <= inventory.min_stock_level
        };
    }
}
