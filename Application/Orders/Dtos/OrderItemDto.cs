namespace Application.Orders.Dtos;

public class OrderItemDto
{
    public int OrderItemId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public string ProductName { get; set; } = null!;
    public string VariantDescription { get; set; } = null!;
    public string Sku { get; set; } = null!;
}
