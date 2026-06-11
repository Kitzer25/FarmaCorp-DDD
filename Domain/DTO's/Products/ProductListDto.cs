namespace Application.Products.Dtos;

public class ProductListDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? GenericName { get; set; }
    public bool RequiresPrescription { get; set; }
    public string CategoryName { get; set; } = null!;
    public string LaboratoryName { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public int Stock { get; set; }
    public string? MainImageUrl { get; set; }
    public string? MainImageAlt { get; set; }
}