using Domain.DTO_s.Inventory;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.Inventory.Querys;

public sealed class GetAvailableStockQuery : IRequest<StockAvailabilityDto>
{
    public int ProductVariantId { get; set; }
}

public sealed class GetAvailableStockQueryHandler : IRequestHandler<GetAvailableStockQuery, StockAvailabilityDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAvailableStockQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StockAvailabilityDto> Handle(GetAvailableStockQuery query, CancellationToken ct)
    {
        var inventory = await _unitOfWork.InventoryRepo.GetByProductVariantIdAsync(query.ProductVariantId, ct);

        if (inventory == null)
        {
            throw new KeyNotFoundException("No existe stock registrado para la variante indicada.");
        }

        var availableQuantity = Math.Max(0, inventory.quantity_on_hand - inventory.reserved_quantity);

        return new StockAvailabilityDto
        {
            ProductVariantId = inventory.product_variant_id,
            QuantityOnHand = inventory.quantity_on_hand,
            ReservedQuantity = inventory.reserved_quantity,
            AvailableQuantity = availableQuantity,
            MinStockLevel = inventory.min_stock_level,
            IsLowStock = availableQuantity <= inventory.min_stock_level
        };
    }
}
