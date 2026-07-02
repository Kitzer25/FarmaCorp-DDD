namespace Domain.DTO_s.DiscountType.Mappers;

public static class DiscountTypeMapper
{
    public static DiscountTypeDto ToDto(this Domain.Entities.DiscountType discountType)
    {
        return new DiscountTypeDto
        {
            Id = discountType.discount_type_id,
            Name = discountType.name
        };
    }
}
