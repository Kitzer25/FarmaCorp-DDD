using Application.CustomerAddresses.Dtos;
using MediatR;

namespace Application.CustomerAddresses.Commands;

public class UpdateCustomerAddressCommand : IRequest<CustomerAddressDto>
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
    public string? Label { get; set; }
    public string? RecipientName { get; set; }
    public string Street { get; set; } = null!;
    public string? District { get; set; }
    public string City { get; set; } = null!;
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public bool IsDefault { get; set; }
}
