using Core.DTO_s.Cart;
using Core.Ports.Services;
using Core.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Cart.Commands;

public class UpdateCartItemQuantityCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
}

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
