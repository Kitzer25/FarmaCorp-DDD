using Application.Cart.Dtos;
using MediatR;

namespace Application.Cart.Commands;

public class RemoveCartItemCommand : IRequest<CartItemRemovedDto>
{
    public int CustomerId { get; set; }
    public int CartItemId { get; set; }
}
