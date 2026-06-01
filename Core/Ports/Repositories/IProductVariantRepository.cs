using Core.Entities;

namespace Core.Ports.Repositories;

public interface IProductVariantRepository :
    IGRepositories<ProductVariant>
{
    Task<ProductVariant?> GetByIdWithProductAsync(int productVariantId, CancellationToken ct);
}
