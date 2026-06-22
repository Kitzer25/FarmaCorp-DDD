namespace Core.DTO_s.Orders;

public class OrderDto
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = null!;
    public int CustomerId { get; set; }
    public int OrderStatusId { get; set; }
    public string? OrderStatus { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}
