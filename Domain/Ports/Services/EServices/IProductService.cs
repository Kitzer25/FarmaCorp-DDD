using Domain.DTO_s.Products;

namespace Domain.Ports.Services.EServices;

public interface IProductService
{
    Task<List<ProductListDto>> GetProductsAsync(ProductQueryParams queryParams, CancellationToken ct);
    Task<ProductDetailDto> GetProductDetailAsync(string slug, CancellationToken ct); // ← agregar
}