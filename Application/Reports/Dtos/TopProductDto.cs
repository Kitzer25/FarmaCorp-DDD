namespace Application.Reports.Dtos;

public class TopProductDto
{
    public int ProductVariantId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public int QuantitySold { get; set; }
    public decimal TotalSold { get; set; }
}
