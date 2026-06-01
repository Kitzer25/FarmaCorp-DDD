namespace Application.CustomerAddresses.Dtos;

public class CustomerAddressDto
{
    public int AddressId { get; set; }
    public int CustomerId { get; set; }
    public string? Label { get; set; }
    public string? RecipientName { get; set; }
    public string Street { get; set; } = null!;
    public string? District { get; set; }
    public string City { get; set; } = null!;
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string Country { get; set; } = null!;
    public string? Phone { get; set; }
    public bool IsDefault { get; set; }
}
