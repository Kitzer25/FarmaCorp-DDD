using Core.DTO_s.Cart;
using Core.Ports.Services;
using Core.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Cart.Querys;

public class GetActiveCartQuery : IRequest<CartDto>
{
    public int CustomerId { get; set; }
}

public sealed class GetActiveCartCommandHandler : IRequestHandler<GetActiveCartQuery, CartDto>
{
    private readonly ICartService _cartService;

    public GetActiveCartCommandHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDto> Handle(GetActiveCartQuery request, CancellationToken cancellationToken)
    {
        return await _cartService.GetActiveCartAsync(request.CustomerId, cancellationToken);
    }
}
