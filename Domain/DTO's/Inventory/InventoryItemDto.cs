namespace Domain.DTO_s.Inventory;

public class InventoryItemDto
{
    public int InventoryId { get; set; }
    public int ProductVariantId { get; set; }
    public string? ProductName { get; set; }
    public string? Sku { get; set; }
    public int QuantityOnHand { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity { get; set; }
    public int MinStockLevel { get; set; }
    public bool IsLowStock { get; set; }
}
