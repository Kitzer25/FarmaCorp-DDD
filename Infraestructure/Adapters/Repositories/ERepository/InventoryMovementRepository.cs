using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class InventoryMovementRepository : 
    GRepositories<InventoryMovement>,
    IInventoryMovementRepository
{ 
    public InventoryMovementRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<IEnumerable<InventoryMovement>> GetByVariantAsync(int productVariantId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(m => m.product_variant_id == productVariantId)

            .OrderByDescending(m => m.created_at)

            .ToListAsync(ct);

    public async Task<IEnumerable<InventoryMovement>> GetByBatchAsync(int batchId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(m => m.batch_id == batchId)

            .OrderByDescending(m => m.created_at)

            .ToListAsync(ct);

    public async Task<IEnumerable<InventoryMovement>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(m => m.created_at >= from && m.created_at <= to)

            .OrderByDescending(m => m.created_at)

            .ToListAsync(ct);

    public async Task<IEnumerable<InventoryMovement>> GetByReferenceAsync(string referenceType, int referenceId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(m => m.reference_type == referenceType && m.reference_id == referenceId)

            .OrderByDescending(m => m.created_at)

            .ToListAsync(ct);

}
