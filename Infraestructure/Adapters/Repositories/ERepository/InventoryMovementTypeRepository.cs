using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

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
