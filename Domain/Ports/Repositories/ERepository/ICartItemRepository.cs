using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ICartItemRepository : 
    IGRepositories<CartItem>
{
    Task<CartItem?> GetByIdWithCartAsync(int cartItemId, CancellationToken ct);
    Task<CartItem?> GetByCartIdAndVariantIdAsync(int cartId, int productVariantId, CancellationToken ct);
    Task<int> CountByCartIdAsync(int cartId, CancellationToken ct);
}
