namespace Domain.DTO_s.ProductImage.Mappers;

public static class ProductImageMapper
{
    public static ProductImageDto ToDto(this Domain.Entities.ProductImage image)
    {
        return new ProductImageDto
        {
            Id = image.product_image_id,
            ProductId = image.product_id,
            ProductVariantId = image.product_variant_id,
            ImageUrl = image.image_url,
            AltText = image.alt_text,
            IsMain = image.is_main,
            SortOrder = image.sort_order,
            CreatedAt = image.created_at
        };
    }

    public static Domain.Entities.ProductImage ToEntity(this CreateProductImageDto dto)
    {
        return new Domain.Entities.ProductImage
        {
            product_id = dto.ProductId,
            product_variant_id = dto.ProductVariantId,
            image_url = dto.ImageUrl,
            alt_text = dto.AltText,
            is_main = dto.IsMain,
            sort_order = dto.SortOrder,
            created_at = DateTime.UtcNow
        };
    }
}
