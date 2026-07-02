using Domain.DTO_s.Inventory;

namespace Domain.Ports.Services.EServices;

public interface IInventoryAdminService
{
    Task<IEnumerable<InventoryItemDto>> GetInventoryAsync(CancellationToken ct);
    Task<InventoryItemDto> RegisterMovementAsync(int? userId, CreateInventoryMovementDto request, CancellationToken ct);
    Task<StockAvailabilityDto> GetAvailableStockAsync(int productVariantId, CancellationToken ct);
}