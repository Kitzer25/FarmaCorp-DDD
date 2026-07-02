namespace Domain.DTO_s.AdminProducts;

public class ProductAdminDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string? GenericName { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public int CategoryId { get; set; }
    public int LaboratoryId { get; set; }
    public bool RequiresPrescription { get; set; }
    public bool IsControlled { get; set; }
    public string? ActiveIngredient { get; set; }
    public string Slug { get; set; } = null!;
    public string? Tags { get; set; }
    public bool IsActive { get; set; }
}
