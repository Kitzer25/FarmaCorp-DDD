using Domain.DTO_s.CustomerWhislist;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CustomerWishlistUseCase.Queries;

public sealed class GETCustomerWishlistQuery : IRequest<IEnumerable<CustomerWishlistDto>>
{
    public int CustomerId { get; set; }
}

public sealed class GETCustomerWishlistQueryHandler : IRequestHandler<GETCustomerWishlistQuery, IEnumerable<CustomerWishlistDto>>
{
    private readonly ICustomerWishlistService _service;

    public GETCustomerWishlistQueryHandler(ICustomerWishlistService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<CustomerWishlistDto>> Handle(GETCustomerWishlistQuery query, CancellationToken ct)
    {
        return await _service.GetByCustomerAsync(query.CustomerId, ct);
    }
}
