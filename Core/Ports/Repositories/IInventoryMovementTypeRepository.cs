using Core.Entities;

namespace Core.Ports.Repositories;

public interface IInventoryMovementTypeRepository : 
    IGRepositories<InventoryMovementType>
{
    Task<InventoryMovementType?> GetActiveByIdAsync(int movementTypeId, CancellationToken ct);
}
