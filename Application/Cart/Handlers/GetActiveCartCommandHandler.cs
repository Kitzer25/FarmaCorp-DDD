using Application.Cart.Commands;
using Application.Cart.Dtos;
using Core.Entities;
using Core.Ports;
using MediatR;

namespace Application.Cart.Handlers;

public class GetActiveCartCommandHandler : IRequestHandler<GetActiveCartCommand, CartDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetActiveCartCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CartDto> Handle(GetActiveCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await GetOrCreateActiveCartAsync(request.CustomerId, cancellationToken);

        return CartMapper.ToDto(cart);
    }

    private async Task<Core.Entities.Cart> GetOrCreateActiveCartAsync(int customerId, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepo.GetActiveByCustomerIdWithItemsAsync(customerId, cancellationToken);

        if (cart != null)
        {
            return cart;
        }

        cart = new Core.Entities.Cart
        {
            customer_id = customerId,
            is_active = true,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.CartRepo.AddAsync(cart, cancellationToken);

        return cart;
    }
}
