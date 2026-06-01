using Application.Cart.Dtos;
using MediatR;

namespace Application.Cart.Commands;

public class GetActiveCartCommand : IRequest<CartDto>
{
    public int CustomerId { get; set; }
}
