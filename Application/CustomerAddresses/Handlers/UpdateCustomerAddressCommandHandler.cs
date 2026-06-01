using Application.CustomerAddresses.Commands;
using Application.CustomerAddresses.Dtos;
using Core.Ports;
using MediatR;

namespace Application.CustomerAddresses.Handlers;

public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, CustomerAddressDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerAddressDto> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await _unitOfWork.CustomerAddressRepo
            .GetActiveByIdAndCustomerIdAsync(request.AddressId, request.CustomerId, cancellationToken);

        if (address == null)
        {
            throw new InvalidOperationException("La dirección no existe o no pertenece al cliente.");
        }

        if (request.IsDefault)
        {
            await _unitOfWork.CustomerAddressRepo.ClearDefaultForCustomerAsync(request.CustomerId, request.AddressId, cancellationToken);
        }

        address.label = request.Label;
        address.recipient_name = request.RecipientName;
        address.street = request.Street.Trim();
        address.district = request.District;
        address.city = request.City.Trim();
        address.state = request.State;
        address.postal_code = request.PostalCode;
        address.country = string.IsNullOrWhiteSpace(request.Country) ? "Peru" : request.Country.Trim();
        address.phone = request.Phone;
        address.is_default = request.IsDefault;
        address.updated_at = DateTime.UtcNow;

        await _unitOfWork.CustomerAddressRepo.UpdateAsync(address.address_id, address, cancellationToken);

        return CustomerAddressMapper.ToDto(address);
    }
}
