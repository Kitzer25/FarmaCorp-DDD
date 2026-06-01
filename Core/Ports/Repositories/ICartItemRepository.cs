using Core.Entities;

namespace Core.Ports.Repositories;

public interface ICartItemRepository : 
    IGRepositories<CartItem>
{
    Task<CartItem?> GetByIdWithCartAsync(int cartItemId, CancellationToken ct);
    Task<CartItem?> GetByCartIdAndVariantIdAsync(int cartId, int productVariantId, CancellationToken ct);
    Task<int> CountByCartIdAsync(int cartId, CancellationToken ct);
}
