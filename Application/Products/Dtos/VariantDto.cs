namespace Application.Products.Dtos;

public class VariantDto
{
    public int ProductVariantId { get; set; }
    public string Sku { get; set; } = null!;
    public string? Barcode { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public int PackageSize { get; set; }
    public string? PackageDescription { get; set; }
    public decimal? Concentration { get; set; }
    public string DrugFormName { get; set; } = null!;
    public string? UnitName { get; set; }
    public int Stock { get; set; }
    public int SortOrder { get; set; }
}