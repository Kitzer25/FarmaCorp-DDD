using Application.Products.Dtos;

namespace Application.Products.Services;

public interface IProductService
{
    Task<List<ProductListDto>> GetProductsAsync(ProductQueryParams queryParams, CancellationToken ct);
}