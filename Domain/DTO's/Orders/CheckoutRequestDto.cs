namespace Core.DTO_s.Orders;

public class CheckoutRequestDto
{
    public int ShippingAddressId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal ShippingCost { get; set; }
    public string? CustomerNotes { get; set; }
}
