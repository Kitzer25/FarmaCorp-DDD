using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IPrescriptionUploadRepository : 
    IGRepositories<PrescriptionUpload>
{
    Task<IEnumerable<PrescriptionUpload>> GetByCustomerIdAsync(int customerId, CancellationToken ct);
    Task<PrescriptionUpload?> GetByIdForCustomerAsync(int prescriptionId, int customerId, CancellationToken ct);
}
