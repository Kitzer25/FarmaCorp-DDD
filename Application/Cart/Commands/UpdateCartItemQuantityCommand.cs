using Application.Cart.Dtos;
using MediatR;

namespace Application.Cart.Commands;

public class UpdateCartItemQuantityCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
}
