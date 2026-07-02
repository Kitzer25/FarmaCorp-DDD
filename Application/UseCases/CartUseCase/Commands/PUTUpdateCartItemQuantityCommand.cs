using Domain.DTO_s.Cart;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CartUseCase.Commands;

public class PUTUpdateCartItemQuantityCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
}

public class PUTUpdateCartItemQuantityCommandHandler : IRequestHandler<PUTUpdateCartItemQuantityCommand, CartDto>
{
    private readonly ICartService _cartService;

    public PUTUpdateCartItemQuantityCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDto> Handle(PUTUpdateCartItemQuantityCommand request, CancellationToken ct)
    {
        return await _cartService.UpdateItemQuantityAsync(
            request.CustomerId,
            request.CartItemId,
            request.Quantity,
            ct);
    }
}
