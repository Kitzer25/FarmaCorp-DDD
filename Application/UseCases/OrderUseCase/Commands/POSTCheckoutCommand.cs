using Domain.DTO_s.Orders;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.OrderUseCase.Commands;

public sealed class POSTCheckoutCommand : IRequest<OrderDto>
{
    public int CustomerId { get; init; }
    public CheckoutRequestDto Request { get; init; } = null!;
}

public sealed class POSTCheckoutCommandHandler : IRequestHandler<POSTCheckoutCommand, OrderDto>
{
    private readonly IOrderService _service;

    public POSTCheckoutCommandHandler(IOrderService service)
    {
        _service = service;
    }

    public async Task<OrderDto> Handle(POSTCheckoutCommand command, CancellationToken ct)
    {
        return await _service.CheckoutAsync(command.CustomerId, command.Request, ct);
    }
}
