using Core.Entities;
using Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class InventoryRepository : 
    GRepositories<Inventory>,
    IInventoryRepository
{ 
    public InventoryRepository(AppDbContext context) : base(context)
    { }

    public async Task<Inventory?> GetByProductVariantIdAsync(int productVariantId, CancellationToken ct)
    {
        return await _context.inventories
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.product_variant_id == productVariantId, ct);
    }
}
