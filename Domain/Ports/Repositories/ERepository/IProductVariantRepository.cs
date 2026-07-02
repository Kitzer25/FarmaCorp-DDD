using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IProductVariantRepository :
    IGRepositories<ProductVariant>
{
    Task<ProductVariant?> GetByIdWithProductAsync(int productVariantId, CancellationToken ct);
}
