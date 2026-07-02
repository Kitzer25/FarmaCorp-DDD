using Domain.Ports;
using Domain.Ports.Services;
using Domain.DTO_s.Products;
using Domain.DTO_s.Products.Mappers;
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

            result.Add(product.ToListDto(variant, stock, mainImage));
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
            .Select(v => v.ToVariantDto())
            .ToList();

        var images = product.product_images
            .OrderByDescending(i => i.is_main)
            .ThenBy(i => i.sort_order)
            .Select(i => i.ToImageDto())
            .ToList();

        return product.ToDetailDto(variants, images);
    }
}