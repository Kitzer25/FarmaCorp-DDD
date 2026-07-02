using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class ProductCategoryRepository : 
    GRepositories<ProductCategory>,
    IProductCategoryRepository
{ 
    public ProductCategoryRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<IEnumerable<ProductCategory>> GetByProductAsync(int productId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .Where(pc => pc.product_id == productId)
            .ToListAsync(ct);

    public async Task<IEnumerable<ProductCategory>> GetByCategoryAsync(int categoryId, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(pc => pc.category_id == categoryId)
            .ToListAsync(ct);

    // Sin AsNoTracking: se usa también para actualizar (SetPrimary).
    public async Task<ProductCategory?> GetByKeyAsync(int productId, int categoryId, CancellationToken ct) =>
        await _dbSet
            .FirstOrDefaultAsync(pc => pc.product_id == productId && pc.category_id == categoryId, ct);

    public async Task<ProductCategory?> GetPrimaryByProductAsync(int productId, CancellationToken ct) =>
        await _dbSet
            .FirstOrDefaultAsync(pc => pc.product_id == productId && pc.is_primary, ct);

    public async Task<bool> ExistsAsync(int productId, int categoryId, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .AnyAsync(pc => pc.product_id == productId && pc.category_id == categoryId, ct);

}
