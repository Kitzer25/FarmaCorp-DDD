using Domain.DTO_s.MeasurementUnit;

namespace Domain.Ports.Services.EServices;

public interface IMeasurementUnitService
{
    Task<List<MeasurementUnitDto>> GetActiveAsync(CancellationToken ct);
}
