using Domain.DTO_s.Inventory;
using Domain.DTO_s.Inventory.Mappers;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class InventoryAdminService : IInventoryAdminService
{
    private readonly IAuditService _auditService;
    private readonly IUnitOfWork _unitOfWork;

    public InventoryAdminService(IUnitOfWork unitOfWork, IAuditService auditService)
    {
        _unitOfWork = unitOfWork;
        _auditService = auditService;
    }

    public async Task<IEnumerable<InventoryItemDto>> GetInventoryAsync(CancellationToken ct)
    {
        var inventory = await _unitOfWork.InventoryRepo.GetAllWithProductAsync(ct);

        return inventory.Select(i => i.ToDto());
    }

    public async Task<InventoryItemDto> RegisterMovementAsync(int? userId, CreateInventoryMovementDto request,
        CancellationToken ct)
    {
        if (request.Quantity <= 0) throw new InvalidOperationException("La cantidad debe ser positiva.");

        var type = await _unitOfWork.InventoryMovementTypeRepo.GetActiveByIdAsync(request.MovementTypeId, ct)
                   ?? throw new InvalidOperationException("El tipo de movimiento no existe.");

        var inventory = await _unitOfWork.InventoryRepo.GetByProductVariantIdAsync(request.ProductVariantId, ct)
                        ?? throw new InvalidOperationException("No existe inventario para la variante indicada.");

        var direction = type.direction.Trim().ToUpperInvariant();
        var newQuantity = direction == "IN"
            ? inventory.quantity_on_hand + request.Quantity
            : inventory.quantity_on_hand - request.Quantity;

        if (newQuantity < 0) throw new InvalidOperationException("El movimiento dejaría stock negativo.");

        var previousQuantity = inventory.quantity_on_hand;
        inventory.quantity_on_hand = newQuantity;
        inventory.last_updated_at = DateTime.UtcNow;

        await _unitOfWork.InventoryRepo.UpdateAsync(inventory.inventory_id, inventory, ct);
        await _unitOfWork.InventoryMovementRepo.AddAsync(new InventoryMovement
        {
            product_variant_id = request.ProductVariantId,
            batch_id = request.BatchId,
            movement_type_id = request.MovementTypeId,
            user_id = userId,
            quantity = request.Quantity,
            reference_type = request.ReferenceType,
            reference_id = request.ReferenceId,
            notes = request.Notes,
            created_at = DateTime.UtcNow
        }, ct);

        await _auditService.RegisterAsync(
            "inventory",
            inventory.inventory_id.ToString(),
            "STOCK_MOVEMENT",
            new { quantity_on_hand = previousQuantity },
            new { quantity_on_hand = newQuantity, movement_type = type.name, request.Quantity },
            userId,
            null,
            ct);

        var updated = await _unitOfWork.InventoryRepo.GetByProductVariantIdAsync(request.ProductVariantId, ct)
                      ?? inventory;

        return updated.ToDto();
    }
}
