namespace Domain.DTO_s.InventoryMovement;

public record InventoryMovementDto

{

    public int Id { get; init; }

    public int ProductVariantId { get; init; }

    public int? BatchId { get; init; }

    public int MovementTypeId { get; init; }

    public int? UserId { get; init; }

    public int Quantity { get; init; }

    public string? ReferenceType { get; init; }

    public int? ReferenceId { get; init; }

    public string? Notes { get; init; }

    public DateTime CreatedAt { get; init; }

}
