using Domain.DTO_s.ProductImage;

namespace Domain.Ports.Services.EServices;

public interface IProductImageService
{
    Task<IEnumerable<ProductImageDto>> GetByProductAsync(int productId, CancellationToken ct);
    Task<ProductImageDto> AddImageAsync(CreateProductImageDto request, CancellationToken ct);
    Task<ProductImageDto> SetMainImageAsync(int productImageId, CancellationToken ct);
    Task<bool> DeleteImageAsync(int productImageId, CancellationToken ct);
}
