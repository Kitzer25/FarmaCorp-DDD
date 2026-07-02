using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

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

    public async Task<CartItem?> GetByCartIdAndVariantIdAsync(int cartId, int productVariantId, CancellationToken ct)
    {
        return await _context.cart_items
            .FirstOrDefaultAsync(ci => ci.cart_id == cartId && ci.product_variant_id == productVariantId, ct);
    }

    public async Task<int> CountByCartIdAsync(int cartId, CancellationToken ct)
    {
        return await _context.cart_items
            .CountAsync(ci => ci.cart_id == cartId, ct);
    }
}
