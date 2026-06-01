using Application.Inventory.Dtos;

namespace Application.Inventory.Services;

public interface IInventoryAdminService
{
    Task<IEnumerable<InventoryItemDto>> GetInventoryAsync(CancellationToken ct);
    Task<InventoryItemDto> RegisterMovementAsync(int? userId, CreateInventoryMovementDto request, CancellationToken ct);
}
