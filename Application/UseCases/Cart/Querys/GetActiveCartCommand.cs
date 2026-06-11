using Application.Cart.Dtos;
using Core.Ports.Services;
using MediatR;

namespace Application.UseCases.Cart.Querys;

public class GetActiveCartCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
}

public sealed class GetActiveCartCommandHandler : IRequestHandler<GetActiveCartCommand, CartDto>
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
