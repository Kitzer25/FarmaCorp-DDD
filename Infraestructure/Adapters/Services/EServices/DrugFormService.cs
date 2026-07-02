using Domain.DTO_s.DrugForm;
using Domain.DTO_s.DrugForm.Mappers;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class DrugFormService : IDrugFormService
{
    private readonly IUnitOfWork _unitOfWork;

    public DrugFormService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<DrugFormDto>> GetActiveAsync(CancellationToken ct)
    {
        var drugForms = await _unitOfWork.DrugFormRepo.GetAllActiveAsync(ct);

        return drugForms.Select(d => d.ToDto()).ToList();
    }
}
