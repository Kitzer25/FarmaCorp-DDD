namespace Domain.DTO_s.OrderPayment;

public sealed record OrderPaymentDto
{
    public int Id { get; init; }

    public int OrderId { get; init; }

    public int PaymentMethodId { get; init; }

    public decimal Amount { get; init; }

    public string? TransactionReference { get; init; }

    public string PaymentStatus { get; init; } = null!;

    public DateTime? PaidAt { get; init; }

    public string? Notes { get; init; }

    public DateTime CreatedAt { get; init; }
}
