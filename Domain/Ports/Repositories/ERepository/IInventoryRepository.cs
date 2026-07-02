using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IInventoryRepository : 
    IGRepositories<Inventory>
{
    Task<Inventory?> GetByProductVariantIdAsync(int productVariantId, CancellationToken ct);
    Task<IEnumerable<Inventory>> GetAllWithProductAsync(CancellationToken ct);
}
