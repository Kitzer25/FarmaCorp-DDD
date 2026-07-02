namespace Domain.DTO_s.OrderStatusHistory;

public sealed record OrderStatusHistoryDto

{
    public int Id { get; init; }

    public int OrderId { get; init; }

    public int OrderStatusId { get; init; }

    public int? ChangedByUserId { get; init; }

    public string? Notes { get; init; }

    public DateTime CreatedAt { get; init; }
}
