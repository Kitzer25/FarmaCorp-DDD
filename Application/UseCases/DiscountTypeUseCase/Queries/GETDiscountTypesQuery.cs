using Domain.DTO_s.DiscountType;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.DiscountTypeUseCase.Queries;

public sealed record GETDiscountTypesQuery : IRequest<List<DiscountTypeDto>>;

public sealed class GETDiscountTypesQueryHandler : IRequestHandler<GETDiscountTypesQuery, List<DiscountTypeDto>>
{
    private readonly IDiscountTypeService _service;

    public GETDiscountTypesQueryHandler(IDiscountTypeService service)
    {
        _service = service;
    }

    public async Task<List<DiscountTypeDto>> Handle(GETDiscountTypesQuery query, CancellationToken ct)
    {
        return await _service.GetAllAsync(ct);
    }
}
