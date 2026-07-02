namespace Domain.DTO_s.Inventory;

public class StockAvailabilityDto
{
    public int ProductVariantId { get; set; }
    public int QuantityOnHand { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity { get; set; }
    public int MinStockLevel { get; set; }
    public bool IsLowStock { get; set; }
}
