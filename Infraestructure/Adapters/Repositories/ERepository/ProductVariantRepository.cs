using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

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
