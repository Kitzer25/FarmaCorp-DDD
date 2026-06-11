using Application.Prescriptions.Dtos;

namespace Core.Ports.Services;

public interface IPrescriptionService
{
    Task<PrescriptionDto> CreateAsync(int customerId, CreatePrescriptionDto request, CancellationToken ct);

    Task<PrescriptionDto> VerifyAsync(int prescriptionId, int userId, VerifyPrescriptionDto request,
        CancellationToken ct);
}