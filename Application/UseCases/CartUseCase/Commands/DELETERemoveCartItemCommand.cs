using Domain.DTO_s.Cart;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CartUseCase.Commands;

public sealed class DELETERemoveCartItemCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
    public int CartItemId { get; set; }
}

public sealed class DELETERemoveCartItemCommandHandler : IRequestHandler<DELETERemoveCartItemCommand, CartDto>
{
    private readonly ICartService _cartService;

    public DELETERemoveCartItemCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDto> Handle(DELETERemoveCartItemCommand request, CancellationToken ct)
    {
        return await _cartService.RemoveItemAsync(request.CustomerId, request.CartItemId, ct);
    }
}
