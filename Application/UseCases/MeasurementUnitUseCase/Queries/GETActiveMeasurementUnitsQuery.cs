using Domain.DTO_s.MeasurementUnit;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.MeasurementUnitUseCase.Queries;

public sealed record GETActiveMeasurementUnitsQuery : IRequest<List<MeasurementUnitDto>>;

public sealed class GETActiveMeasurementUnitsQueryHandler : IRequestHandler<GETActiveMeasurementUnitsQuery, List<MeasurementUnitDto>>
{
    private readonly IMeasurementUnitService _service;

    public GETActiveMeasurementUnitsQueryHandler(IMeasurementUnitService service)
    {
        _service = service;
    }

    public async Task<List<MeasurementUnitDto>> Handle(GETActiveMeasurementUnitsQuery query, CancellationToken ct)
    {
        return await _service.GetActiveAsync(ct);
    }
}
