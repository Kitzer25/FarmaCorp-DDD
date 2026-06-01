using Application.CustomerAddresses.Commands;
using Application.CustomerAddresses.Dtos;
using Core.Entities;
using Core.Ports;
using MediatR;

namespace Application.CustomerAddresses.Handlers;

public class CreateCustomerAddressCommandHandler : IRequestHandler<CreateCustomerAddressCommand, CustomerAddressDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerAddressDto> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        if (request.IsDefault)
        {
            await _unitOfWork.CustomerAddressRepo.ClearDefaultForCustomerAsync(request.CustomerId, null, cancellationToken);
        }

        var address = new CustomerAddress
        {
            customer_id = request.CustomerId,
            label = request.Label,
            recipient_name = request.RecipientName,
            street = request.Street.Trim(),
            district = request.District,
            city = request.City.Trim(),
            state = request.State,
            postal_code = request.PostalCode,
            country = string.IsNullOrWhiteSpace(request.Country) ? "Peru" : request.Country.Trim(),
            phone = request.Phone,
            is_default = request.IsDefault,
            is_active = true,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.CustomerAddressRepo.AddAsync(address, cancellationToken);

        return CustomerAddressMapper.ToDto(address);
    }
}
