using Domain.DTO_s.Promotions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Promotion.Querys;

public sealed record GetActivePromotionsQuery : IRequest<IEnumerable<PromotionDto>>;

public sealed class GetActivePromotionsQueryHandler : IRequestHandler<GetActivePromotionsQuery, IEnumerable<PromotionDto>>
{
    private readonly IPromotionService _promotionService;

    public GetActivePromotionsQueryHandler(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    public async Task<IEnumerable<PromotionDto>> Handle(GetActivePromotionsQuery query, CancellationToken ct)
    {
        return await _promotionService.GetActiveAsync(ct);
    }
}
