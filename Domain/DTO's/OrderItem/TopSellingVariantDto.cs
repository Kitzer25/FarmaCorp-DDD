namespace Domain.DTO_s.OrderItem;

public sealed record TopSellingVariantDto

{
    public int ProductVariantId { get; init; }

    public int TotalQuantity { get; init; }

    public decimal TotalRevenue { get; init; }
}
