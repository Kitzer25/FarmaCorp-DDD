using Domain.Entities;

namespace Domain.DTO_s.Prescriptions.Mappers;

public static class PrescriptionMapper
{
    public static PrescriptionDto ToDto(this PrescriptionUpload prescription)
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
