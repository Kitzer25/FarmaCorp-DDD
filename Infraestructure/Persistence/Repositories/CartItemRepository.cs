using Core.Entities;
using Core.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class CartItemRepository : 
    GRepositories<CartItem>,
    ICartItemRepository
{ 
    public CartItemRepository(AppDbContext context) : base(context)
    { }

    public async Task<CartItem?> GetByIdWithCartAsync(int cartItemId, CancellationToken ct)
    {
        return await _context.cart_items
            .Include(ci => ci.cart)
            .FirstOrDefaultAsync(ci => ci.cart_item_id == cartItemId, ct);
    }

    public async Task<int> CountByCartIdAsync(int cartId, CancellationToken ct)
    {
        return await _context.cart_items
            .CountAsync(ci => ci.cart_id == cartId, ct);
    }
}
