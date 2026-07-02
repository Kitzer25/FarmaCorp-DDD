using Domain.DTO_s.CustomerAddresses;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.CustomerAddress.Commands;

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
