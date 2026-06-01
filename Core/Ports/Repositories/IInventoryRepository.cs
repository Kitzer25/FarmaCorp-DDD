using Core.Entities;

namespace Core.Ports.Repositories;

public interface IInventoryRepository : 
    IGRepositories<Inventory>
{
    Task<Inventory?> GetByProductVariantIdAsync(int productVariantId, CancellationToken ct);
}
