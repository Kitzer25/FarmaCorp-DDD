using Domain.Ports;
using Domain.Ports.Services;
using Domain.DTO_s.Cart;
using Domain.DTO_s.Cart.Mappers;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CartDto> GetActiveCartAsync(int customerId, CancellationToken ct)
    {
        var cart = await GetOrCreateActiveCartAsync(customerId, ct);
        return CartMapper.ToDto(cart);
    }

    public async Task<CartDto> AddItemAsync(int customerId, int productVariantId, int quantity, CancellationToken ct)
    {
        ValidateQuantity(quantity);

        var variant = await GetAvailableVariantAsync(productVariantId, ct);
        var cart = await GetOrCreateActiveCartAsync(customerId, ct);
        var existingItem = await _unitOfWork.CartItemRepo.GetByCartIdAndVariantIdAsync(cart.cart_id, productVariantId, ct);

        var newQuantity = quantity + (existingItem?.quantity ?? 0);
        await EnsureStockAvailableAsync(productVariantId, newQuantity, ct);

        if (existingItem == null)
        {
            await AddNewItemAsync(cart.cart_id, productVariantId, quantity, variant.price, ct);
        }
        else
        {
            await UpdateExistingItemAsync(existingItem, newQuantity, variant.price, ct);
        }

        await _unitOfWork.SaveChangesAsync(ct);

        return await GetActiveCartAsync(customerId, ct);
    }

    public async Task<CartDto> UpdateItemQuantityAsync(int customerId, int cartItemId, int quantity, CancellationToken ct)
    {
        ValidateQuantity(quantity);

        var cartItem = await GetOwnedCartItemAsync(customerId, cartItemId, ct);

        await EnsureStockAvailableAsync(cartItem.product_variant_id, quantity, ct);

        cartItem.quantity = quantity;
        cartItem.updated_at = DateTime.UtcNow;

        await _unitOfWork.CartItemRepo.UpdateAsync(cartItem.cart_item_id, cartItem, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return await GetActiveCartAsync(customerId, ct);
    }

    public async Task<CartDto> RemoveItemAsync(int customerId, int cartItemId, CancellationToken ct)
    {
        var cartItem = await GetOwnedCartItemAsync(customerId, cartItemId, ct);

        await _unitOfWork.CartItemRepo.DeleteAsync(cartItem, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return await GetActiveCartAsync(customerId, ct);
    }

    private async Task<Cart> GetOrCreateActiveCartAsync(int customerId, CancellationToken ct)
    {
        var cart = await _unitOfWork.CartRepo.GetActiveByCustomerIdWithItemsAsync(customerId, ct);

        if (cart != null)
        {
            return cart;
        }

        cart = new Cart
        {
            customer_id = customerId,
            is_active = true,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.CartRepo.AddAsync(cart, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return cart;
    }

    private async Task<ProductVariant> GetAvailableVariantAsync(int productVariantId, CancellationToken ct)
    {
        var variant = await _unitOfWork.ProductVariantRepo.GetByIdAsync(productVariantId, ct);

        if (variant == null || !variant.is_active || variant.deleted_at != null)
        {
            throw new InvalidOperationException("La variante del producto no existe o no está disponible.");
        }

        return variant;
    }

    private async Task<CartItem> GetOwnedCartItemAsync(int customerId, int cartItemId, CancellationToken ct)
    {
        var cartItem = await _unitOfWork.CartItemRepo.GetByIdWithCartAsync(cartItemId, ct);

        if (cartItem == null)
        {
            throw new InvalidOperationException("El item del carrito no existe.");
        }

        if (cartItem.cart.customer_id != customerId)
        {
            throw new UnauthorizedAccessException("No puedes modificar items de otro cliente.");
        }

        return cartItem;
    }

    private async Task EnsureStockAvailableAsync(int productVariantId, int requestedQuantity, CancellationToken ct)
    {
        var inventory = await _unitOfWork.InventoryRepo.GetByProductVariantIdAsync(productVariantId, ct);
        var availableQuantity = inventory == null
            ? 0
            : Math.Max(0, inventory.quantity_on_hand - inventory.reserved_quantity);

        if (requestedQuantity > availableQuantity)
        {
            throw new InvalidOperationException("No hay stock suficiente para la cantidad solicitada.");
        }
    }

    private async Task AddNewItemAsync(int cartId, int productVariantId, int quantity, decimal unitPrice, CancellationToken ct)
    {
        await _unitOfWork.CartItemRepo.AddAsync(new CartItem
        {
            cart_id = cartId,
            product_variant_id = productVariantId,
            quantity = quantity,
            unit_price_snapshot = unitPrice,
            added_at = DateTime.UtcNow
        }, ct);
    }

    private async Task UpdateExistingItemAsync(CartItem item, int quantity, decimal unitPrice, CancellationToken ct)
    {
        item.quantity = quantity;
        item.unit_price_snapshot = unitPrice;
        item.updated_at = DateTime.UtcNow;

        await _unitOfWork.CartItemRepo.UpdateAsync(item.cart_item_id, item, ct);
    }

    private static void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("La cantidad debe ser mayor a 0.");
        }
    }
}