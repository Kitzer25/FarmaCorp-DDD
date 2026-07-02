using Domain.DTO_s.Laboratory;
using Domain.DTO_s.Laboratory.Mappers;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class LaboratoryService : ILaboratoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public LaboratoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<LaboratoryDto>> GetActiveAsync(CancellationToken ct)
    {
        var laboratories = await _unitOfWork.LaboratoryRepo.GetAllActiveAsync(ct);

        return laboratories.Select(l => l.ToDto()).ToList();
    }
}
