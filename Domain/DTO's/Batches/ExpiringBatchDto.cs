namespace Domain.DTO_s.Batches;

public class ExpiringBatchDto
{
    public int? BatchId { get; set; }
    public string? BatchNumber { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public int? DaysUntilExpiry { get; set; }
    public int? CurrentQuantity { get; set; }
    public string? Sku { get; set; }
    public string? ProductName { get; set; }
    public string? DrugForm { get; set; }
}
