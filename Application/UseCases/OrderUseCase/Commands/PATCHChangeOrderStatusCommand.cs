using Domain.DTO_s.Orders;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.OrderUseCase.Commands;

public sealed class PATCHChangeOrderStatusCommand : IRequest<OrderDto>
{
    public int OrderId { get; init; }
    public int StatusId { get; init; }
    public int? ChangedByUserId { get; init; }
    public string? Notes { get; init; }
}

public sealed class PATCHChangeOrderStatusCommandHandler : IRequestHandler<PATCHChangeOrderStatusCommand, OrderDto>
{
    private readonly IOrderService _service;

    public PATCHChangeOrderStatusCommandHandler(IOrderService service)
    {
        _service = service;
    }

    public async Task<OrderDto> Handle(PATCHChangeOrderStatusCommand command, CancellationToken ct)
    {
        return await _service.ChangeStatusAsync(command.OrderId, command.StatusId, command.ChangedByUserId, command.Notes, ct);
    }
}
