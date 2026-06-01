using Application.Cart.Commands;
using Application.Cart.Dtos;
using Core.Ports;
using MediatR;

namespace Application.Cart.Handlers;

public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, CartDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCartItemQuantityCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CartDto> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
        {
            throw new InvalidOperationException("La cantidad debe ser mayor a 0.");
        }

        var cartItem = await _unitOfWork.CartItemRepo.GetByIdWithCartAsync(request.CartItemId, cancellationToken);

        if (cartItem == null)
        {
            throw new InvalidOperationException("El item del carrito no existe.");
        }

        if (cartItem.cart.customer_id != request.CustomerId)
        {
            throw new UnauthorizedAccessException("No puedes actualizar items de otro cliente.");
        }

        await EnsureStockAvailableAsync(cartItem.product_variant_id, request.Quantity, cancellationToken);

        cartItem.quantity = request.Quantity;
        cartItem.updated_at = DateTime.UtcNow;

        await _unitOfWork.CartItemRepo.UpdateAsync(cartItem.cart_item_id, cartItem, cancellationToken);

        var cart = await _unitOfWork.CartRepo.GetActiveByCustomerIdWithItemsAsync(request.CustomerId, cancellationToken)
            ?? throw new InvalidOperationException("No se pudo obtener el carrito activo.");

        return CartMapper.ToDto(cart);
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
}
