using Core.DTO_s.Cart;
using Core.Ports.Services;
using Core.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Cart.Commands;

public class AddCartItemCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
}

public sealed class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, CartDto>
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