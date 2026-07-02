using Domain.DTO_s.CustomerAddresses;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CustomerAddressUseCase.Commands;

public class SetDefaultCustomerAddressCommand : IRequest<CustomerAddressDto>
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
}

public sealed class SetDefaultCustomerAddressCommandHandler : IRequestHandler<SetDefaultCustomerAddressCommand, CustomerAddressDto>
{
    private readonly ICustomerAddressService _customerAddressService;

    public SetDefaultCustomerAddressCommandHandler(ICustomerAddressService customerAddressService)
    {
        _customerAddressService = customerAddressService;
    }

    public Task<CustomerAddressDto> Handle(SetDefaultCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        return _customerAddressService.SetDefaultAsync(request.CustomerId, request.AddressId, cancellationToken);
    }
}
