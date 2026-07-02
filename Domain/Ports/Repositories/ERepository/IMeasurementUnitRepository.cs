using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IMeasurementUnitRepository :
    IGRepositories<MeasurementUnit>
{
    Task<MeasurementUnit?> GetBySymbolAsync(string symbol, CancellationToken ct);

    Task<IEnumerable<MeasurementUnit>> GetAllActiveAsync(CancellationToken ct);

    Task<bool> ExistsBySymbolAsync(string symbol, CancellationToken ct);

}