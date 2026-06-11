using Application.Products.Dtos;

namespace Core.Ports.Services;

public interface IProductService
{
    Task<List<ProductListDto>> GetProductsAsync(ProductQueryParams queryParams, CancellationToken ct);
    Task<ProductDetailDto> GetProductDetailAsync(string slug, CancellationToken ct); // ← agregar
}