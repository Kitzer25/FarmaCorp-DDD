using Domain.DTO_s.Inventory;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Inventory.Commands;

public sealed class RegisterInventoryMovementCommand : IRequest<InventoryItemDto>
{
    public int? UserId { get; init; }
    public CreateInventoryMovementDto Request { get; init; } = null!;
}

public sealed class RegisterInventoryMovementCommandHandler : IRequestHandler<RegisterInventoryMovementCommand, InventoryItemDto>
{
    private readonly IInventoryAdminService _service;

    public RegisterInventoryMovementCommandHandler(IInventoryAdminService service)
    {
        _service = service;
    }

    public async Task<InventoryItemDto> Handle(RegisterInventoryMovementCommand command, CancellationToken ct)
    {
        return await _service.RegisterMovementAsync(command.UserId, command.Request, ct);
    }
}