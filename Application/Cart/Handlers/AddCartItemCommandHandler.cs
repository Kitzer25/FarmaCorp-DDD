using Application.Cart.Commands;
using Application.Cart.Dtos;
using Application.Cart.Services;
using MediatR;

namespace Application.Cart.Handlers;

public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, CartDto>
{
    private readonly ICartService _cartService;

    public AddCartItemCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDto> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        return await _cartService.AddItemAsync(
            request.CustomerId,
            request.ProductVariantId,
            request.Quantity,
            cancellationToken);
    }
}
