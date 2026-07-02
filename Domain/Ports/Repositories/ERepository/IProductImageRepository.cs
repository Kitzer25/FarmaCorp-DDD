using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IProductImageRepository : 
    IGRepositories<ProductImage>
{
    Task<IEnumerable<ProductImage>> GetByProductAsync(int productId, CancellationToken ct);

    Task<IEnumerable<ProductImage>> GetByVariantAsync(int productVariantId, CancellationToken ct);

    Task<ProductImage?> GetMainByProductAsync(int productId, CancellationToken ct);

}