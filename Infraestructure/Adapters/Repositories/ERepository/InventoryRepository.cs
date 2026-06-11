using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class InventoryRepository : 
    GRepositories<Inventory>,
    IInventoryRepository
{ 
    public InventoryRepository(AppDbContext context) : base(context)
    { }

    public async Task<Inventory?> GetByProductVariantIdAsync(int productVariantId, CancellationToken ct)
    {
        return await _context.inventories
            .FirstOrDefaultAsync(i => i.product_variant_id == productVariantId, ct);
    }

    public async Task<IEnumerable<Inventory>> GetAllWithProductAsync(CancellationToken ct)
    {
        return await _context.inventories
            .AsNoTracking()
            .Include(i => i.product_variant)
                .ThenInclude(v => v.product)
            .OrderBy(i => i.product_variant.product.name)
            .ThenBy(i => i.product_variant.sku)
            .ToListAsync(ct);
    }
}
