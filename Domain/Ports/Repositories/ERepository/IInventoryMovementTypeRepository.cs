using Core.Entities;

namespace Core.Ports.Repositories.ERepository;

public interface IInventoryMovementTypeRepository : 
    IGRepositories<InventoryMovementType>
{
    Task<InventoryMovementType?> GetActiveByIdAsync(int movementTypeId, CancellationToken ct);
}
