using Domain.DTO_s.MeasurementUnit;
using Domain.DTO_s.MeasurementUnit.Mappers;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class MeasurementUnitService : IMeasurementUnitService
{
    private readonly IUnitOfWork _unitOfWork;

    public MeasurementUnitService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MeasurementUnitDto>> GetActiveAsync(CancellationToken ct)
    {
        var units = await _unitOfWork.MeasurementUnitRepo.GetAllActiveAsync(ct);

        return units.Select(u => u.ToDto()).ToList();
    }
}
