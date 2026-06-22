using Core.DTO_s.Cart;
using Core.Ports;
using Core.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Cart.Commands;

public class RemoveCartItemCommand : IRequest<CartItemRemovedDto>
{
    public int CustomerId { get; set; }
    public int CartItemId { get; set; }
}

public sealed class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, CartItemRemovedDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCartItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CartItemRemovedDto> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _unitOfWork.CartItemRepo
            .GetByIdWithCartAsync(request.CartItemId, cancellationToken);

        if (cartItem == null)
        {
            throw new InvalidOperationException("El item del carrito no existe.");
        }

        if (cartItem.cart.customer_id != request.CustomerId)
        {
            throw new UnauthorizedAccessException("No puedes eliminar items de otro cliente.");
        }

        var cartId = cartItem.cart_id;

        await _unitOfWork.CartItemRepo.DeleteAsync(cartItem, cancellationToken);

        var remainingItems = await _unitOfWork.CartItemRepo
            .CountByCartIdAsync(cartId, cancellationToken);

        return new CartItemRemovedDto
        {
            CartId = cartId,
            RemainingItems = remainingItems,
            IsCartEmpty = remainingItems == 0
        };
    }
}
