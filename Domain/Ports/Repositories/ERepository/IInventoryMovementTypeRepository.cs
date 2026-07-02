using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IInventoryMovementTypeRepository : 
    IGRepositories<InventoryMovementType>
{
    Task<InventoryMovementType?> GetActiveByIdAsync(int movementTypeId, CancellationToken ct);
}
