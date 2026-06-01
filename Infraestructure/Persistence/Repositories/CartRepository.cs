using Core.Entities;
using Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class CartRepository : 
    GRepositories<Cart>,
    ICartRepository
{ 
    public CartRepository(AppDbContext context) : base(context)
    { }

    public async Task<Cart?> GetActiveByCustomerIdWithItemsAsync(int customerId, CancellationToken ct)
    {
        return await _context.carts
            .Include(c => c.cart_items)
                .ThenInclude(ci => ci.product_variant)
                    .ThenInclude(pv => pv.product)
            .FirstOrDefaultAsync(c => c.customer_id == customerId && c.is_active, ct);
    }
}
