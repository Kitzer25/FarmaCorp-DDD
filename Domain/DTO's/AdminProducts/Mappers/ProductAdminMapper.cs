using Domain.Entities;

namespace Domain.DTO_s.AdminProducts.Mappers;

public static class ProductAdminMapper
{
    public static ProductAdminDto ToDto(this Product product)
    {
        return new ProductAdminDto
        {
            ProductId = product.product_id,
            Name = product.name,
            GenericName = product.generic_name,
            Description = product.description,
            ShortDescription = product.short_description,
            CategoryId = product.category_id,
            LaboratoryId = product.laboratory_id,
            RequiresPrescription = product.requires_prescription,
            IsControlled = product.is_controlled,
            ActiveIngredient = product.active_ingredient,
            Slug = product.slug,
            Tags = product.tags,
            IsActive = product.is_active
        };
    }
}
