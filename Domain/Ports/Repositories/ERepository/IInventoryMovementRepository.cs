using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IInventoryMovementRepository : 
    IGRepositories<InventoryMovement>
{
    Task<IEnumerable<InventoryMovement>> GetByVariantAsync(int productVariantId, CancellationToken ct);

    Task<IEnumerable<InventoryMovement>> GetByBatchAsync(int batchId, CancellationToken ct);

    Task<IEnumerable<InventoryMovement>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct);

    Task<IEnumerable<InventoryMovement>> GetByReferenceAsync(string referenceType, int referenceId, CancellationToken ct);

}