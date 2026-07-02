namespace Domain.DTO_s.CustomerWhislist.Mappers;

public static class CustomerWishlistMapper
{
    public static CustomerWishlistDto ToDto(this Domain.Entities.CustomerWishlist wishlistItem)
    {
        return new CustomerWishlistDto
        {
            CustomerId = wishlistItem.customer_id,
            ProductVariantId = wishlistItem.product_variant_id,
            AddedAt = wishlistItem.added_at
        };
    }

    public static MostWishlistedVariantDto ToDto(this (int ProductVariantId, int Total) item)
    {
        return new MostWishlistedVariantDto
        {
            ProductVariantId = item.ProductVariantId,
            TimesWishlisted = item.Total
        };
    }
}
