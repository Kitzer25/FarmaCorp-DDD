using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class CustomerWishlistRepository : 
    GRepositories<CustomerWishlist>,
    ICustomerWishlistRepository
{ 
    public CustomerWishlistRepository(AppDbContext context) : base(context)
    { }

    public async Task<IEnumerable<CustomerWishlist>> GetByCustomerAsync(int customerId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(w => w.customer_id == customerId)

            .OrderByDescending(w => w.added_at)

            .ToListAsync(ct);

    public async Task<CustomerWishlist?> GetByKeyAsync(int customerId, int productVariantId, CancellationToken ct) =>

        await _dbSet

            .FirstOrDefaultAsync(w => w.customer_id == customerId && w.product_variant_id == productVariantId, ct);

    public async Task<bool> ExistsAsync(int customerId, int productVariantId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .AnyAsync(w => w.customer_id == customerId && w.product_variant_id == productVariantId, ct);

    public async Task<IEnumerable<(int ProductVariantId, int Total)>> GetMostWishlistedAsync(int top, CancellationToken ct)

    {

        var result = await _dbSet.AsNoTracking()

            .GroupBy(w => w.product_variant_id)

            .Select(g => new { ProductVariantId = g.Key, Total = g.Count() })

            .OrderByDescending(x => x.Total)

            .Take(top)

            .ToListAsync(ct);

        return result.Select(x => (x.ProductVariantId, x.Total));

    }

}
