using Core.Entities;
using Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class CategoryRepository :
    GRepositories<Category>,
    ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Category>> GetActiveAsync(CancellationToken ct)
    {
        return await _context.categories
            .AsNoTracking()
            .Where(c => c.is_active)
            .OrderBy(c => c.sort_order)
            .ToListAsync(ct);
    }
}