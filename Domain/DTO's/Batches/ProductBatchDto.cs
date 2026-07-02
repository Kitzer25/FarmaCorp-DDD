namespace Domain.DTO_s.Batches;

public class ProductBatchDto
{
    public int BatchId { get; set; }
    public int ProductVariantId { get; set; }
    public string? ProductName { get; set; }
    public string? Sku { get; set; }
    public int? LaboratoryId { get; set; }
    public string BatchNumber { get; set; } = null!;
    public DateOnly? ManufactureDate { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public int InitialQuantity { get; set; }
    public int CurrentQuantity { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
}
