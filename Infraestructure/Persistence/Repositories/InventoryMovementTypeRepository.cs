using Core.Entities;
using Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class InventoryMovementTypeRepository : 
    GRepositories<InventoryMovementType>,
    IInventoryMovementTypeRepository
{ 
    public InventoryMovementTypeRepository(AppDbContext context) : base(context)
    { }

    public async Task<InventoryMovementType?> GetActiveByIdAsync(int movementTypeId, CancellationToken ct)
    {
        return await _context.inventory_movement_types
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.movement_type_id == movementTypeId && t.is_active, ct);
    }
}
