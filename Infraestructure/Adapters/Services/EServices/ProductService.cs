using Domain.Ports;
using Domain.Ports.Services;
using Domain.DTO_s.Products;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProductListDto>> GetProductsAsync(ProductQueryParams p, CancellationToken ct)
    {
        if (p.MinPrice.HasValue && p.MaxPrice.HasValue && p.MinPrice > p.MaxPrice)
            throw new ArgumentException("El precio mínimo no puede ser mayor que el precio máximo.");

        var products = await _unitOfWork.ProductRepo.GetActiveAsync(ct);
        var query = products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(p.Search))
        {
            var term = p.Search.Trim().ToLower();
            query = query.Where(x =>
                x.name.ToLower().Contains(term) ||
                (x.generic_name != null && x.generic_name.ToLower().Contains(term)) ||
                (x.active_ingredient != null && x.active_ingredient.ToLower().Contains(term)) ||
                (x.tags != null && x.tags.ToLower().Contains(term)));
        }

        if (p.CategoryId.HasValue)
            query = query.Where(x => x.category_id == p.CategoryId.Value);

        if (p.LaboratoryId.HasValue)
            query = query.Where(x => x.laboratory_id == p.LaboratoryId.Value);

        if (!string.IsNullOrWhiteSpace(p.ActiveIngredient))
        {
            var ai = p.ActiveIngredient.Trim().ToLower();
            query = query.Where(x =>
                x.active_ingredient != null &&
                x.active_ingredient.ToLower().Contains(ai));
        }

        var result = new List<ProductListDto>();

        foreach (var product in query)
        {
            var variant = product.product_variants
                .Where(v => v.is_active && v.deleted_at == null)
                .OrderBy(v => v.sort_order)
                .FirstOrDefault();

            if (variant == null) continue;

            if (p.MinPrice.HasValue && variant.price < p.MinPrice.Value) continue;
            if (p.MaxPrice.HasValue && variant.price > p.MaxPrice.Value) continue;

            var stock = variant.inventory != null
                ? variant.inventory.quantity_on_hand - variant.inventory.reserved_quantity
                : 0;

            var mainImage = product.product_images
                .Where(i => i.product_id == product.product_id)
                .OrderByDescending(i => i.is_main)
                .ThenBy(i => i.sort_order)
                .FirstOrDefault();

            result.Add(new ProductListDto
            {
                ProductId            = product.product_id,
                Name                 = product.name,
                Slug                 = product.slug,
                GenericName          = product.generic_name,
                RequiresPrescription = product.requires_prescription,
                CategoryName         = product.category.name,
                LaboratoryName       = product.laboratory.name,
                Price                = variant.price,
                CompareAtPrice       = variant.compare_at_price,
                Stock                = stock,
                MainImageUrl         = mainImage?.image_url,
                MainImageAlt         = mainImage?.alt_text
            });
        }

        return result;
    }
    public async Task<ProductDetailDto> GetProductDetailAsync(string slug, CancellationToken ct)
    {
        var product = await _unitOfWork.ProductRepo.GetBySlugAsync(slug, ct);

        if (product == null || product.deleted_at != null || !product.is_active)
            throw new KeyNotFoundException($"Producto '{slug}' no encontrado.");

        var variants = product.product_variants
            .Where(v => v.is_active && v.deleted_at == null)
            .OrderBy(v => v.sort_order)
            .Select(v => new VariantDto
            {
                ProductVariantId  = v.product_variant_id,
                Sku               = v.sku,
                Barcode           = v.barcode,
                Price             = v.price,
                CompareAtPrice    = v.compare_at_price,
                PackageSize       = v.package_size,
                PackageDescription = v.package_description,
                Concentration     = v.concentration,
                DrugFormName      = v.drug_form.name,
                UnitName          = v.unit?.name,
                Stock             = v.inventory != null
                                        ? v.inventory.quantity_on_hand - v.inventory.reserved_quantity
                                        : 0,
                SortOrder         = v.sort_order
            }).ToList();

        var images = product.product_images
            .OrderByDescending(i => i.is_main)
            .ThenBy(i => i.sort_order)
            .Select(i => new ImageDto
            {
                ProductImageId   = i.product_image_id,
                ImageUrl         = i.image_url,
                AltText          = i.alt_text,
                IsMain           = i.is_main,
                SortOrder        = i.sort_order,
                ProductVariantId = i.product_variant_id
            }).ToList();

        return new ProductDetailDto
        {
            ProductId            = product.product_id,
            Name                 = product.name,
            Slug                 = product.slug,
            GenericName          = product.generic_name,
            Description          = product.description,
            ShortDescription     = product.short_description,
            ActiveIngredient     = product.active_ingredient,
            RequiresPrescription = product.requires_prescription,
            IsControlled         = product.is_controlled,
            CategoryName         = product.category.name,
            LaboratoryName       = product.laboratory.name,
            Variants             = variants,
            Images               = images
        };
    }    
}