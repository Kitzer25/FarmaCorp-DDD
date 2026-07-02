namespace Domain.DTO_s.Products;

public class ProductQueryParams
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? LaboratoryId { get; set; }
    public string? ActiveIngredient { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}