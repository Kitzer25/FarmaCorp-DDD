namespace Application.Products.Dtos;

public class ImageDto
{
    public int ProductImageId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
    public bool IsMain { get; set; }
    public int SortOrder { get; set; }
    public int? ProductVariantId { get; set; }
}