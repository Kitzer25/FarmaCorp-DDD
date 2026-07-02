using Domain.DTO_s.CustomerWhislist;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CustomerWishlistUseCase.Queries;

public sealed class GETMostWishlistedQuery : IRequest<IEnumerable<MostWishlistedVariantDto>>
{
    public int Top { get; set; }
}

public sealed class GETMostWishlistedQueryHandler : IRequestHandler<GETMostWishlistedQuery, IEnumerable<MostWishlistedVariantDto>>
{
    private readonly ICustomerWishlistService _service;

    public GETMostWishlistedQueryHandler(ICustomerWishlistService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<MostWishlistedVariantDto>> Handle(GETMostWishlistedQuery query, CancellationToken ct)
    {
        return await _service.GetMostWishlistedAsync(query.Top, ct);
    }
}
