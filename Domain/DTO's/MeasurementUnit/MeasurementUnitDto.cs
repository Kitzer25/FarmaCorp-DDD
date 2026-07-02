namespace Domain.DTO_s.MeasurementUnit;

public sealed record MeasurementUnitDto

{

    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Symbol { get; init; } = null!;

    public bool IsActive { get; init; }

}
