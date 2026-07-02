namespace Domain.DTO_s.DrugForm;

public record DrugFormDto

{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public bool IsActive { get; init; }
}
