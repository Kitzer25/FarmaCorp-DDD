using Application.Cart.Commands;
using Application.Cart.Dtos;
using Application.Cart.Services;
using MediatR;

namespace Application.Cart.Handlers;

public class GetActiveCartCommandHandler : IRequestHandler<GetActiveCartCommand, CartDto>
{
    private readonly ICartService _cartService;

    public GetActiveCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDto> Handle(GetActiveCartCommand request, CancellationToken cancellationToken)
    {
        return await _cartService.GetActiveCartAsync(request.CustomerId, cancellationToken);
    }
}
