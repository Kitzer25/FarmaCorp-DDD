using Domain.DTO_s.Inventory;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.InventoryUseCase.Queries;

public sealed class GetAvailableStockQuery : IRequest<StockAvailabilityDto>
{
    public int ProductVariantId { get; set; }
}

public sealed class GetAvailableStockQueryHandler : IRequestHandler<GetAvailableStockQuery, StockAvailabilityDto>
{
    private readonly IInventoryAdminService _service;

    public GetAvailableStockQueryHandler(IInventoryAdminService service)
    {
        _service = service;
    }

    public Task<StockAvailabilityDto> Handle(GetAvailableStockQuery query, CancellationToken ct)
    {
        return _service.GetAvailableStockAsync(query.ProductVariantId, ct);
    }
}
