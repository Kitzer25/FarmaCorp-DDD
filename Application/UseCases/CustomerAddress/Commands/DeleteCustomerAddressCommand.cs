using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.CustomerAddress.Commands;

public class DeleteCustomerAddressCommand : IRequest
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
}


public sealed class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _unitOfWork.CustomerAddressRepo
            .GetActiveByIdAndCustomerIdAsync(request.AddressId, request.CustomerId, cancellationToken);

        if (address == null)
        {
            throw new InvalidOperationException("La dirección no existe o no pertenece al cliente.");
        }

        address.is_active = false;
        address.is_default = false;
        address.updated_at = DateTime.UtcNow;

        await _unitOfWork.CustomerAddressRepo.UpdateAsync(address.address_id, address, cancellationToken);
    }
}