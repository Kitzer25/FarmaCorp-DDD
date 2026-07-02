namespace Domain.DTO_s.Batches;

public class CreateProductBatchDto
{
    public int ProductVariantId { get; set; }
    public int? LaboratoryId { get; set; }
    public string BatchNumber { get; set; } = null!;
    public DateOnly? ManufactureDate { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public int InitialQuantity { get; set; }
    public string? Notes { get; set; }
}
