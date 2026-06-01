using Application.CustomerAddresses.Dtos;
using MediatR;

namespace Application.CustomerAddresses.Commands;

public class SetDefaultCustomerAddressCommand : IRequest<CustomerAddressDto>
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
}
