using Core.DTO_s.CustomerAddresses;
using Core.Ports;
using Core.Ports.Repositories;
using MediatR;

namespace Application.UseCases.CustomerAddress.Commands;

public class CreateCustomerAddressCommand : IRequest<CustomerAddressDto>
{
    public int CustomerId { get; set; }
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

        var address = new Core.Entities.CustomerAddress
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
