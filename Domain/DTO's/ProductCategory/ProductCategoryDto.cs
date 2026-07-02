namespace Domain.DTO_s.ProductCategory;

public record ProductCategoryDto
{
    public int ProductId { get; init; }

    public int CategoryId { get; init; }

    public bool IsPrimary { get; init; }

    public DateTime AssignedAt { get; init; }
}
