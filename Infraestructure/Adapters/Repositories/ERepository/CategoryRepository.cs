using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

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