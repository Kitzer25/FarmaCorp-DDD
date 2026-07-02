using Domain.Ports;
using Domain.Ports.Services;
using Domain.DTO_s.Prescriptions;
using Domain.DTO_s.Prescriptions.Mappers;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class PrescriptionService : IPrescriptionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditService _auditService;

    public PrescriptionService(IUnitOfWork unitOfWork, IAuditService auditService)
    {
        _unitOfWork = unitOfWork;
        _auditService = auditService;
    }

    public async Task<PrescriptionDto> CreateAsync(int customerId, CreatePrescriptionDto request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.ImageUrl))
        {
            throw new InvalidOperationException("La imagen de la receta es obligatoria.");
        }

        var prescription = new PrescriptionUpload
        {
            customer_id = customerId,
            order_id = request.OrderId,
            image_url = request.ImageUrl.Trim(),
            doctor_name = request.DoctorName,
            doctor_license = request.DoctorLicense,
            issued_date = request.IssuedDate,
            notes = request.Notes,
            is_verified = false,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.PrescriptionUploadRepo.AddAsync(prescription, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return prescription.ToDto();
    }

    public async Task<PrescriptionDto> VerifyAsync(int prescriptionId, int userId, VerifyPrescriptionDto request, CancellationToken ct)
    {
        if (!request.Approve && string.IsNullOrWhiteSpace(request.RejectionReason))
        {
            throw new InvalidOperationException("Debe indicar un motivo para rechazar la receta.");
        }

        var prescription = await _unitOfWork.PrescriptionUploadRepo.GetByIdAsync(prescriptionId, ct);

        if (prescription == null)
        {
            throw new InvalidOperationException("La receta no existe.");
        }

        var before = prescription.ToDto();
        prescription.is_verified = request.Approve;
        prescription.verified_by_user_id = userId;
        prescription.verified_at = DateTime.UtcNow;
        prescription.rejection_reason = request.Approve ? null : request.RejectionReason;
        prescription.notes = request.Notes ?? prescription.notes;
        prescription.updated_at = DateTime.UtcNow;

        await _unitOfWork.PrescriptionUploadRepo.UpdateAsync(prescription.prescription_id, prescription, ct);
        await _auditService.RegisterAsync(
            "prescription_uploads",
            prescription.prescription_id.ToString(),
            request.Approve ? "APPROVE" : "REJECT",
            before,
            prescription.ToDto(),
            userId,
            prescription.customer_id,
            ct);

        return prescription.ToDto();
    }
}