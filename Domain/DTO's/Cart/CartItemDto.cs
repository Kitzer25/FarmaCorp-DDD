namespace Application.Cart.Dtos;

public class CartItemDto
{
    public int CartItemId { get; set; }
    public int ProductVariantId { get; set; }
    public string? ProductName { get; set; }
    public string? Sku { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}
