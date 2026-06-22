using Core.DTO_s.Prescriptions;
using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Core.Ports.Services;
using Core.Ports.Services.EServices;

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

        return ToDto(prescription);
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

        var before = ToDto(prescription);
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
            ToDto(prescription),
            userId,
            prescription.customer_id,
            ct);

        return ToDto(prescription);
    }

    private static PrescriptionDto ToDto(PrescriptionUpload prescription)
    {
        return new PrescriptionDto
        {
            PrescriptionId = prescription.prescription_id,
            CustomerId = prescription.customer_id,
            OrderId = prescription.order_id,
            ImageUrl = prescription.image_url,
            DoctorName = prescription.doctor_name,
            DoctorLicense = prescription.doctor_license,
            IssuedDate = prescription.issued_date,
            IsVerified = prescription.is_verified,
            VerifiedByUserId = prescription.verified_by_user_id,
            VerifiedAt = prescription.verified_at,
            RejectionReason = prescription.rejection_reason,
            Notes = prescription.notes
        };
    }
}
