namespace Core.DTO_s.Products;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public int? ParentCategoryId { get; set; }
    public int SortOrder { get; set; }
    public List<CategoryDto> SubCategories { get; set; } = new();
}