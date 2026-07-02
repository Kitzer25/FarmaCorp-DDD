using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CustomerAddressUseCase.Commands;

public class DeleteCustomerAddressCommand : IRequest
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
}

public sealed class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand>
{
    private readonly ICustomerAddressService _customerAddressService;

    public DeleteCustomerAddressCommandHandler(ICustomerAddressService customerAddressService)
    {
        _customerAddressService = customerAddressService;
    }

    public Task Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        return _customerAddressService.DeleteAsync(request.CustomerId, request.AddressId, cancellationToken);
    }
}
