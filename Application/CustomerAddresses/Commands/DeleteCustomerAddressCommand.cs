using MediatR;

namespace Application.CustomerAddresses.Commands;

public class DeleteCustomerAddressCommand : IRequest
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
}
