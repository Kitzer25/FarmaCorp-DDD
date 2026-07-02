using Domain.DTO_s.Inventory;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.InventoryUseCase.Queries;

public sealed record GETInventoryQuery : IRequest<IEnumerable<InventoryItemDto>>;

public sealed class GETInventoryQueryHandler : IRequestHandler<GETInventoryQuery, IEnumerable<InventoryItemDto>>
{
    private readonly IInventoryAdminService _service;

    public GETInventoryQueryHandler(IInventoryAdminService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<InventoryItemDto>> Handle(GETInventoryQuery query, CancellationToken ct)
    {
        return await _service.GetInventoryAsync(ct);
    }
}
