namespace Domain.DTO_s.DiscountType;

public record DiscountTypeDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}
