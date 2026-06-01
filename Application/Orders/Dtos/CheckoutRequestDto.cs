namespace Application.Orders.Dtos;

public class CheckoutRequestDto
{
    public int ShippingAddressId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal ShippingCost { get; set; }
    public string? CustomerNotes { get; set; }
}
