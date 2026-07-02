namespace Domain.DTO_s.Laboratory;

public record LaboratoryDto
{

    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string? CountryOfOrigin { get; init; }

    public string? ContactEmail { get; init; }

    public string? Phone { get; init; }

    public string? Website { get; init; }

    public bool IsActive { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

}
