using Core.DTO_s.Inventory;
using Core.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Inventory.Querys;

public sealed record GetInventoryQuery : IRequest<IEnumerable<InventoryItemDto>>;

public sealed class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, IEnumerable<InventoryItemDto>>
{
    private readonly IInventoryAdminService _service;

    public GetInventoryQueryHandler(IInventoryAdminService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<InventoryItemDto>> Handle(GetInventoryQuery query, CancellationToken ct)
    {
        return await _service.GetInventoryAsync(ct);
    }
}