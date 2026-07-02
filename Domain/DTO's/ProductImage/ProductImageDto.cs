namespace Domain.DTO_s.ProductImage;

public record ProductImageDto
{
    public int Id { get; init; }

    public int? ProductId { get; init; }

    public int? ProductVariantId { get; init; }

    public string ImageUrl { get; init; } = null!;

    public string? AltText { get; init; }

    public bool IsMain { get; init; }

    public int SortOrder { get; init; }

    public DateTime CreatedAt { get; init; }
}
