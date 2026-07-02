using Domain.DTO_s.Orders;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Order.Commands;

public sealed class ChangeOrderStatusCommand : IRequest<OrderDto>
{
    public int OrderId { get; init; }
    public int StatusId { get; init; }
    public int? ChangedByUserId { get; init; }
    public string? Notes { get; init; }
}

public sealed class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, OrderDto>
{
    private readonly IOrderService _service;

    public ChangeOrderStatusCommandHandler(IOrderService service)
    {
        _service = service;
    }

    public async Task<OrderDto> Handle(ChangeOrderStatusCommand command, CancellationToken ct)
    {
        return await _service.ChangeStatusAsync(command.OrderId, command.StatusId, command.ChangedByUserId, command.Notes, ct);
    }
}