using Core.Entities;
using Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class ProductRepository : 
    GRepositories<Product>,
    IProductRepository
{ 
    public ProductRepository(AppDbContext context) : base(context)
    { }

    public async Task<Product?> GetBySlugAsync(string slug, CancellationToken ct)
    {
        return await _context.products
            .FirstOrDefaultAsync(p => p.slug == slug && p.deleted_at == null, ct);
    }

    public async Task<IEnumerable<Product>> GetActiveAsync(CancellationToken ct)
    {
        return await _context.products
            .AsNoTracking()
            .Where(p => p.deleted_at == null && p.is_active)
            .Include(p => p.category)
            .Include(p => p.laboratory)
            .Include(p => p.product_images)
            .Include(p => p.product_variants)
            .ThenInclude(v => v.inventory)
            .OrderBy(p => p.name)
            .ToListAsync(ct);
    }
}
