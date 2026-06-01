using Core.Entities;
using Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class ProductVariantRepository : 
    GRepositories<ProductVariant>,
    IProductVariantRepository
{ 
    public ProductVariantRepository(AppDbContext context) : base(context)
    { }

    public async Task<ProductVariant?> GetByIdWithProductAsync(int productVariantId, CancellationToken ct)
    {
        return await _context.product_variants
            .Include(v => v.product)
            .FirstOrDefaultAsync(v => v.product_variant_id == productVariantId, ct);
    }
}
