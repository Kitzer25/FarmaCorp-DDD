namespace Domain.DTO_s.OrderItem;

public sealed record OrderItemDto

{
    public int Id { get; init; }

    public int OrderId { get; init; }

    public int ProductVariantId { get; init; }

    public int? BatchId { get; init; }

    public int? PrescriptionId { get; init; }

    public int Quantity { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal DiscountAmount { get; init; }

    public decimal Subtotal { get; init; }

    public string ProductNameSnapshot { get; init; } = null!;

    public string VariantDescSnapshot { get; init; } = null!;

    public string SkuSnapshot { get; init; } = null!;
}
