using Domain.DTO_s.Promotions;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Promotion.Querys;

public sealed class ValidateCouponQuery : IRequest<CouponValidationDto>
{
    public string Code { get; set; } = null!;
    public decimal OrderSubtotal { get; set; }
}

public sealed class ValidateCouponQueryHandler : IRequestHandler<ValidateCouponQuery, CouponValidationDto>
{
    private readonly IPromotionService _promotionService;

    public ValidateCouponQueryHandler(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    public async Task<CouponValidationDto> Handle(ValidateCouponQuery query, CancellationToken ct)
    {
        return await _promotionService.ValidateCouponAsync(query.Code, query.OrderSubtotal, ct);
    }
}
