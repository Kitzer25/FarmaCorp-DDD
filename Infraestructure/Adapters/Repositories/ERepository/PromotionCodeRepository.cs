using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class PromotionCodeRepository : 
    GRepositories<PromotionCode>,
    IPromotionCodeRepository
{ 
    public PromotionCodeRepository(AppDbContext context) : base(context)
    { }

    public async Task<PromotionCode?> GetActiveByCodeWithPromotionAsync(string code, CancellationToken ct)
    {
        var normalized = code.Trim().ToUpperInvariant();

        return await _context.promotion_codes
            .Include(c => c.promotion)
                .ThenInclude(p => p.discount_type)
            .FirstOrDefaultAsync(c => c.code == normalized && c.is_active, ct);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct)
    {
        var normalized = code.Trim().ToUpperInvariant();

        return await _context.promotion_codes
            .AnyAsync(c => c.code == normalized, ct);
    }
}
