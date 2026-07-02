using Domain.DTO_s.Cart;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CartUseCase.Commands;

public class POSTAddCartItemCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
}

public sealed class POSTAddCartItemCommandHandler : IRequestHandler<POSTAddCartItemCommand, CartDto>
{
    private readonly ICartService _cartService;

    public POSTAddCartItemCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDto> Handle(POSTAddCartItemCommand request, CancellationToken ct)
    {
        return await _cartService.AddItemAsync(
            request.CustomerId,
            request.ProductVariantId,
            request.Quantity,
            ct);
    }
}
