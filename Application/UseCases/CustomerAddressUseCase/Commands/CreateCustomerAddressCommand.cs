using Domain.DTO_s.CustomerAddresses;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CustomerAddressUseCase.Commands;

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
    private readonly ICustomerAddressService _customerAddressService;

    public CreateCustomerAddressCommandHandler(ICustomerAddressService customerAddressService)
    {
        _customerAddressService = customerAddressService;
    }

    public Task<CustomerAddressDto> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        return _customerAddressService.CreateAsync(request.CustomerId, new SaveCustomerAddressDto
        {
            Label = request.Label,
            RecipientName = request.RecipientName,
            Street = request.Street,
            District = request.District,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            Phone = request.Phone,
            IsDefault = request.IsDefault
        }, cancellationToken);
    }
}
