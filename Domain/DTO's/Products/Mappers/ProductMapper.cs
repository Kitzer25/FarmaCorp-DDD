using Domain.Entities;

namespace Domain.DTO_s.Products.Mappers;

public static class ProductMapper
{
    public static ProductListDto ToListDto(this Product product, ProductVariant variant, int stock, Domain.Entities.ProductImage? mainImage)
    {
        return new ProductListDto
        {
            ProductId = product.product_id,
            Name = product.name,
            Slug = product.slug,
            GenericName = product.generic_name,
            RequiresPrescription = product.requires_prescription,
            CategoryName = product.category.name,
            LaboratoryName = product.laboratory.name,
            Price = variant.price,
            CompareAtPrice = variant.compare_at_price,
            Stock = stock,
            MainImageUrl = mainImage?.image_url,
            MainImageAlt = mainImage?.alt_text
        };
    }

    public static VariantDto ToVariantDto(this ProductVariant variant)
    {
        return new VariantDto
        {
            ProductVariantId = variant.product_variant_id,
            Sku = variant.sku,
            Barcode = variant.barcode,
            Price = variant.price,
            CompareAtPrice = variant.compare_at_price,
            PackageSize = variant.package_size,
            PackageDescription = variant.package_description,
            Concentration = variant.concentration,
            DrugFormName = variant.drug_form.name,
            UnitName = variant.unit?.name,
            Stock = variant.inventory != null
                ? variant.inventory.quantity_on_hand - variant.inventory.reserved_quantity
                : 0,
            SortOrder = variant.sort_order
        };
    }

    public static ImageDto ToImageDto(this Domain.Entities.ProductImage image)
    {
        return new ImageDto
        {
            ProductImageId = image.product_image_id,
            ImageUrl = image.image_url,
            AltText = image.alt_text,
            IsMain = image.is_main,
            SortOrder = image.sort_order,
            ProductVariantId = image.product_variant_id
        };
    }

    public static ProductDetailDto ToDetailDto(this Product product, List<VariantDto> variants, List<ImageDto> images)
    {
        return new ProductDetailDto
        {
            ProductId = product.product_id,
            Name = product.name,
            Slug = product.slug,
            GenericName = product.generic_name,
            Description = product.description,
            ShortDescription = product.short_description,
            ActiveIngredient = product.active_ingredient,
            RequiresPrescription = product.requires_prescription,
            IsControlled = product.is_controlled,
            CategoryName = product.category.name,
            LaboratoryName = product.laboratory.name,
            Variants = variants,
            Images = images
        };
    }
}
