using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class PromotionRepository : 
    GRepositories<Promotion>,
    IPromotionRepository
{ 
    public PromotionRepository(AppDbContext context) : base(context)
    { }

    public async Task<IEnumerable<Promotion>> GetActiveAsync(CancellationToken ct)
    {
        return await _context.promotions
            .AsNoTracking()
            .Include(p => p.discount_type)
            .Include(p => p.promotion_codes)
            .Where(p => p.is_active)
            .OrderByDescending(p => p.created_at)
            .ToListAsync(ct);
    }
}
