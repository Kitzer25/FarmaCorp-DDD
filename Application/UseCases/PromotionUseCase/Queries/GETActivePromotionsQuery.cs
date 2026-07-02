using Domain.DTO_s.Promotions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.PromotionUseCase.Queries;

public sealed record GETActivePromotionsQuery : IRequest<IEnumerable<PromotionDto>>;

public sealed class GETActivePromotionsQueryHandler : IRequestHandler<GETActivePromotionsQuery, IEnumerable<PromotionDto>>
{
    private readonly IPromotionService _promotionService;

    public GETActivePromotionsQueryHandler(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    public async Task<IEnumerable<PromotionDto>> Handle(GETActivePromotionsQuery query, CancellationToken ct)
    {
        return await _promotionService.GetActiveAsync(ct);
    }
}
