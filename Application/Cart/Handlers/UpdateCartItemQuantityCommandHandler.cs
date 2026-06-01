using Application.Cart.Commands;
using Application.Cart.Dtos;
using Application.Cart.Services;
using MediatR;

namespace Application.Cart.Handlers;

public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, CartDto>
{
    private readonly ICartService _cartService;

    public UpdateCartItemQuantityCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDto> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        return await _cartService.UpdateItemQuantityAsync(
            request.CustomerId,
            request.CartItemId,
            request.Quantity,
            cancellationToken);
    }
}
