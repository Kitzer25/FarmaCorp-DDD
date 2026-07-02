using Domain.DTO_s.Cart;

namespace Domain.Ports.Services.EServices;

public interface ICartService
{
    Task<CartDto> GetActiveCartAsync(int customerId, CancellationToken ct);
    Task<CartDto> AddItemAsync(int customerId, int productVariantId, int quantity, CancellationToken ct);
    Task<CartDto> UpdateItemQuantityAsync(int customerId, int cartItemId, int quantity, CancellationToken ct);
    Task<CartDto> RemoveItemAsync(int customerId, int cartItemId, CancellationToken ct);
}
