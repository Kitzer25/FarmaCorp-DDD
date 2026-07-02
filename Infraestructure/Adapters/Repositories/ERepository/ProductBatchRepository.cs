using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class ProductBatchRepository : 
    GRepositories<ProductBatch>,
    IProductBatchRepository
{ 
    public ProductBatchRepository(AppDbContext context) : base(context)
    { }

    public async Task<IEnumerable<ProductBatch>> GetAllWithProductAsync(CancellationToken ct)
    {
        return await _context.product_batches
            .AsNoTracking()
            .Include(b => b.product_variant)
                .ThenInclude(v => v.product)
            .OrderBy(b => b.expiration_date)
            .ThenBy(b => b.batch_number)
            .ToListAsync(ct);
    }

    public async Task<ProductBatch?> GetByVariantAndBatchNumberAsync(int productVariantId, string batchNumber, CancellationToken ct)
    {
        return await _context.product_batches
            .AsNoTracking()
            .FirstOrDefaultAsync(b =>
                b.product_variant_id == productVariantId &&
                b.batch_number == batchNumber,
                ct);
    }
}
