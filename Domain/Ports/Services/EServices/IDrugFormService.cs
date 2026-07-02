using Domain.DTO_s.DrugForm;

namespace Domain.Ports.Services.EServices;

public interface IDrugFormService
{
    Task<List<DrugFormDto>> GetActiveAsync(CancellationToken ct);
}
