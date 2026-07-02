using Domain.Entities;

namespace Domain.DTO_s.Promotions.Mappers;

public static class PromotionMapper
{
    public static PromotionDto ToDto(this Promotion promotion)
    {
        return new PromotionDto
        {
            PromotionId = promotion.promotion_id,
            Name = promotion.name,
            Description = promotion.description,
            DiscountTypeId = promotion.discount_type_id,
            DiscountValue = promotion.discount_value,
            MinOrderAmount = promotion.min_order_amount,
            MaxDiscountAmount = promotion.max_discount_amount,
            MaxUses = promotion.max_uses,
            CurrentUses = promotion.current_uses,
            StartDate = promotion.start_date,
            EndDate = promotion.end_date,
            IsActive = promotion.is_active,
            Codes = promotion.promotion_codes.Select(c => c.code).ToList()
        };
    }
}
