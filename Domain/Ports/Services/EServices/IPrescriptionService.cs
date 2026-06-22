using Core.DTO_s.Prescriptions;

namespace Core.Ports.Services.EServices;

public interface IPrescriptionService
{
    Task<PrescriptionDto> CreateAsync(int customerId, CreatePrescriptionDto request, CancellationToken ct);

    Task<PrescriptionDto> VerifyAsync(int prescriptionId, int userId, VerifyPrescriptionDto request,
        CancellationToken ct);
}