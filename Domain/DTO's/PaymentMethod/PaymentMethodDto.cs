namespace Domain.DTO_s.PaymentMethod;

public sealed record PaymentMethodDto

{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public bool IsOnline { get; init; }

    public bool IsActive { get; init; }
}
