using Application.Cart.Dtos;
using MediatR;

namespace Application.Cart.Commands;

public class AddCartItemCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
}
