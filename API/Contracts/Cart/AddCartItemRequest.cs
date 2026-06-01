namespace API.Contracts.Cart;

public class AddCartItemRequest
{
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
}
