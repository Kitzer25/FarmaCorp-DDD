namespace Domain.DTO_s.Cart;

public class CartItemDto
{
    public int CartItemId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal? UnitPriceSnapshot { get; set; }
}

