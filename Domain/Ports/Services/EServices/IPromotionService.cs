using Core.DTO_s.Promotions;

namespace Core.Ports.Services.EServices;

public interface IPromotionService
{
    Task<IEnumerable<PromotionDto>> GetActiveAsync(CancellationToken ct);
    Task<PromotionDto> CreateAsync(CreatePromotionDto request, CancellationToken ct);
    Task<CouponValidationDto> ValidateCouponAsync(string code, decimal orderSubtotal, CancellationToken ct);
}