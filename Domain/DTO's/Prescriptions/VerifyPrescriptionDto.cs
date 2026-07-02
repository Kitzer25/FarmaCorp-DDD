namespace Domain.DTO_s.Prescriptions;

public class VerifyPrescriptionDto
{
    public bool Approve { get; set; }
    public string? RejectionReason { get; set; }
    public string? Notes { get; set; }
}
