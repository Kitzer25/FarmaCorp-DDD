namespace Application.Products.Dtos;

public class ProductDetailDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? GenericName { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? ActiveIngredient { get; set; }
    public bool RequiresPrescription { get; set; }
    public bool IsControlled { get; set; }
    public string CategoryName { get; set; } = null!;
    public string LaboratoryName { get; set; } = null!;

    public List<VariantDto> Variants { get; set; } = new();
    public List<ImageDto> Images { get; set; } = new();
}