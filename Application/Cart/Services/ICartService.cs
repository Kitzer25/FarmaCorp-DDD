using Application.Cart.Dtos;

namespace Application.Cart.Services;

public interface ICartService
{
    Task<CartDto> GetActiveCartAsync(int customerId, CancellationToken ct);
    Task<CartDto> AddItemAsync(int customerId, int productVariantId, int quantity, CancellationToken ct);
    Task<CartDto> UpdateItemQuantityAsync(int customerId, int cartItemId, int quantity, CancellationToken ct);
}
