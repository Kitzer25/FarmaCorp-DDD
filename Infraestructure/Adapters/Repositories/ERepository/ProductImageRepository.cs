using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class ProductImageRepository : 
    GRepositories<ProductImage>,
    IProductImageRepository
{ 
    public ProductImageRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<IEnumerable<ProductImage>> GetByProductAsync(int productId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .Where(i => i.product_id == productId)
            .OrderBy(i => i.sort_order)
            .ToListAsync(ct);

    public async Task<IEnumerable<ProductImage>> GetByVariantAsync(int productVariantId, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(i => i.product_variant_id == productVariantId)
            .OrderBy(i => i.sort_order)
            .ToListAsync(ct);

    public async Task<ProductImage?> GetMainByProductAsync(int productId, CancellationToken ct) =>
        await _dbSet
            .FirstOrDefaultAsync(i => i.product_id == productId && i.is_main, ct);
}
