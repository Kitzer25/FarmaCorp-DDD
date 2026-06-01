using Application.Cart.Commands;
using Application.Cart.Dtos;
using Core.Entities;
using Core.Ports;
using MediatR;

namespace Application.Cart.Handlers;

public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, CartDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCartItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CartDto> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
        {
            throw new InvalidOperationException("La cantidad debe ser mayor a 0.");
        }

        var variant = await _unitOfWork.ProductVariantRepo.GetByIdAsync(request.ProductVariantId, cancellationToken);

        if (variant == null || !variant.is_active || variant.deleted_at != null)
        {
            throw new InvalidOperationException("La variante del producto no existe o no está disponible.");
        }

        var cart = await GetOrCreateActiveCartAsync(request.CustomerId, cancellationToken);
        var existingItem = await _unitOfWork.CartItemRepo
            .GetByCartIdAndVariantIdAsync(cart.cart_id, request.ProductVariantId, cancellationToken);

        var newQuantity = request.Quantity + (existingItem?.quantity ?? 0);

        await EnsureStockAvailableAsync(request.ProductVariantId, newQuantity, cancellationToken);

        if (existingItem == null)
        {
            await _unitOfWork.CartItemRepo.AddAsync(new CartItem
            {
                cart_id = cart.cart_id,
                product_variant_id = request.ProductVariantId,
                quantity = request.Quantity,
                unit_price_snapshot = variant.price,
                added_at = DateTime.UtcNow
            }, cancellationToken);
        }
        else
        {
            existingItem.quantity = newQuantity;
            existingItem.unit_price_snapshot = variant.price;
            existingItem.updated_at = DateTime.UtcNow;

            await _unitOfWork.CartItemRepo.UpdateAsync(existingItem.cart_item_id, existingItem, cancellationToken);
        }

        return await GetCartDtoAsync(request.CustomerId, cancellationToken);
    }

    private async Task<Core.Entities.Cart> GetOrCreateActiveCartAsync(int customerId, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepo.GetActiveByCustomerIdWithItemsAsync(customerId, cancellationToken);

        if (cart != null)
        {
            return cart;
        }

        cart = new Core.Entities.Cart
        {
            customer_id = customerId,
            is_active = true,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.CartRepo.AddAsync(cart, cancellationToken);

        return cart;
    }

    private async Task EnsureStockAvailableAsync(int productVariantId, int requestedQuantity, CancellationToken cancellationToken)
    {
        var inventory = await _unitOfWork.InventoryRepo.GetByProductVariantIdAsync(productVariantId, cancellationToken);
        var availableQuantity = inventory == null
            ? 0
            : Math.Max(0, inventory.quantity_on_hand - inventory.reserved_quantity);

        if (requestedQuantity > availableQuantity)
        {
            throw new InvalidOperationException("No hay stock suficiente para la cantidad solicitada.");
        }
    }

    private async Task<CartDto> GetCartDtoAsync(int customerId, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepo.GetActiveByCustomerIdWithItemsAsync(customerId, cancellationToken)
            ?? throw new InvalidOperationException("No se pudo obtener el carrito activo.");

        return CartMapper.ToDto(cart);
    }
}
