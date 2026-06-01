namespace Application.Cart.Dtos;

public class CartItemRemovedDto
{
    public int CartId { get; set; }
    public int RemainingItems { get; set; }
    public bool IsCartEmpty { get; set; }
}
