namespace Application.Prescriptions.Dtos;

public class PrescriptionDto
{
    public int PrescriptionId { get; set; }
    public int CustomerId { get; set; }
    public int? OrderId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? DoctorName { get; set; }
    public string? DoctorLicense { get; set; }
    public DateOnly? IssuedDate { get; set; }
    public bool IsVerified { get; set; }
    public int? VerifiedByUserId { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? RejectionReason { get; set; }
    public string? Notes { get; set; }
}
