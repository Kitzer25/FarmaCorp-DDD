namespace Domain.DTO_s.Prescriptions;

public class CreatePrescriptionDto
{
    public int? OrderId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? DoctorName { get; set; }
    public string? DoctorLicense { get; set; }
    public DateOnly? IssuedDate { get; set; }
    public string? Notes { get; set; }
}
