using Core.Entities;

namespace Core.Ports.Repositories.ERepository;

public interface IInventoryRepository : 
    IGRepositories<Inventory>
{
    Task<Inventory?> GetByProductVariantIdAsync(int productVariantId, CancellationToken ct);
    Task<IEnumerable<Inventory>> GetAllWithProductAsync(CancellationToken ct);
}
