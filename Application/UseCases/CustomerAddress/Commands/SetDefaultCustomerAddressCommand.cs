using Application.CustomerAddresses.Dtos;
using Core.Ports;
using MediatR;

namespace Application.UseCases.CustomerAddress.Commands;

public class SetDefaultCustomerAddressCommand : IRequest<CustomerAddressDto>
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
}

public sealed class SetDefaultCustomerAddressCommandHandler : IRequestHandler<SetDefaultCustomerAddressCommand, CustomerAddressDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public SetDefaultCustomerAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerAddressDto> Handle(SetDefaultCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _unitOfWork.CustomerAddressRepo
            .GetActiveByIdAndCustomerIdAsync(request.AddressId, request.CustomerId, cancellationToken);

        if (address == null)
        {
            throw new InvalidOperationException("La dirección no existe o no pertenece al cliente.");
        }

        await _unitOfWork.CustomerAddressRepo.ClearDefaultForCustomerAsync(request.CustomerId, request.AddressId, cancellationToken);

        address.is_default = true;
        address.updated_at = DateTime.UtcNow;

        await _unitOfWork.CustomerAddressRepo.UpdateAsync(address.address_id, address, cancellationToken);

        return CustomerAddressMapper.ToDto(address);
    }
}