using Application.Inventory.Dtos;

namespace Core.Ports.Services;

public interface IInventoryAdminService
{
    Task<IEnumerable<InventoryItemDto>> GetInventoryAsync(CancellationToken ct);
    Task<InventoryItemDto> RegisterMovementAsync(int? userId, CreateInventoryMovementDto request, CancellationToken ct);
}