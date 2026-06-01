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
            .Where(p => p.deleted_at == null)
            .OrderBy(p => p.name)
            .ToListAsync(ct);
    }
}
