using Domain.DTO_s.CustomerAddresses;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.CustomerAddress.Querys;

public sealed class GetCustomerAddressesQuery : IRequest<IEnumerable<CustomerAddressDto>>
{
    public int CustomerId { get; set; }
}

public sealed class GetCustomerAddressesQueryHandler : IRequestHandler<GetCustomerAddressesQuery, IEnumerable<CustomerAddressDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerAddressesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CustomerAddressDto>> Handle(GetCustomerAddressesQuery query, CancellationToken ct)
    {
        var addresses = await _unitOfWork.CustomerAddressRepo.GetActiveByCustomerIdAsync(query.CustomerId, ct);

        return addresses.Select(CustomerAddressMapper.ToDto);
    }
}
