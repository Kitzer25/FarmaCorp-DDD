using Core.DTO_s.Orders;
using Core.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Order.Commands;

public sealed class CheckoutCommand : IRequest<OrderDto>
{
    public int CustomerId { get; init; }
    public CheckoutRequestDto Request { get; init; } = null!;
}

public sealed class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, OrderDto>
{
    private readonly IOrderService _service;

    public CheckoutCommandHandler(IOrderService service)
    {
        _service = service;
    }

    public async Task<OrderDto> Handle(CheckoutCommand command, CancellationToken ct)
    {
        return await _service.CheckoutAsync(command.CustomerId, command.Request, ct);
    }
}