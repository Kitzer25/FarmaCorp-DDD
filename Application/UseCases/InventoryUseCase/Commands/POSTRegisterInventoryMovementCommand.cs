using Domain.DTO_s.Inventory;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.InventoryUseCase.Commands;

public sealed class PostRegisterInventoryMovementCommand : IRequest<InventoryItemDto>
{
    public int? UserId { get; init; }
    public CreateInventoryMovementDto Request { get; init; } = null!;
}

public sealed class PostRegisterInventoryMovementCommandHandler : IRequestHandler<PostRegisterInventoryMovementCommand, InventoryItemDto>
{
    private readonly IInventoryAdminService _service;

    public PostRegisterInventoryMovementCommandHandler(IInventoryAdminService service)
    {
        _service = service;
    }

    public async Task<InventoryItemDto> Handle(PostRegisterInventoryMovementCommand command, CancellationToken ct)
    {
        return await _service.RegisterMovementAsync(command.UserId, command.Request, ct);
    }
}
