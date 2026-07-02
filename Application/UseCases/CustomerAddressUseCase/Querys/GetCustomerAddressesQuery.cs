using Domain.DTO_s.CustomerAddresses;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CustomerAddressUseCase.Querys;

public sealed class GetCustomerAddressesQuery : IRequest<IEnumerable<CustomerAddressDto>>
{
    public int CustomerId { get; set; }
}

public sealed class GetCustomerAddressesQueryHandler : IRequestHandler<GetCustomerAddressesQuery, IEnumerable<CustomerAddressDto>>
{
    private readonly ICustomerAddressService _customerAddressService;

    public GetCustomerAddressesQueryHandler(ICustomerAddressService customerAddressService)
    {
        _customerAddressService = customerAddressService;
    }

    public Task<IEnumerable<CustomerAddressDto>> Handle(GetCustomerAddressesQuery query, CancellationToken ct)
    {
        return _customerAddressService.GetActiveByCustomerAsync(query.CustomerId, ct);
    }
}
