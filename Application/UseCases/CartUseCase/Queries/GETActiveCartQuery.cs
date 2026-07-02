using Domain.DTO_s.Cart;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CartUseCase.Queries;

public class GETActiveCartQuery : IRequest<CartDto>
{
    public int CustomerId { get; set; }
}

public sealed class GETActiveCartQueryHandler : IRequestHandler<GETActiveCartQuery, CartDto>
{
    private readonly ICartService _cartService;

    public GETActiveCartQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<CartDto> Handle(GETActiveCartQuery request, CancellationToken ct)
    {
        return await _cartService.GetActiveCartAsync(request.CustomerId, ct);
    }
}
