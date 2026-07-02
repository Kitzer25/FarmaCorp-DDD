using Domain.DTO_s.Laboratory;

namespace Domain.Ports.Services.EServices;

public interface ILaboratoryService
{
    Task<List<LaboratoryDto>> GetActiveAsync(CancellationToken ct);
}
