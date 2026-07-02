using Domain.DTO_s.Promotions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.PromotionUseCase.Queries;

public sealed class POSTValidateCouponQuery : IRequest<CouponValidationDto>
{
    public string Code { get; set; } = null!;
    public decimal OrderSubtotal { get; set; }
}

public sealed class POSTValidateCouponQueryHandler : IRequestHandler<POSTValidateCouponQuery, CouponValidationDto>
{
    private readonly IPromotionService _promotionService;

    public POSTValidateCouponQueryHandler(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    public async Task<CouponValidationDto> Handle(POSTValidateCouponQuery query, CancellationToken ct)
    {
        return await _promotionService.ValidateCouponAsync(query.Code, query.OrderSubtotal, ct);
    }
}
