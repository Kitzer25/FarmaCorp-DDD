namespace Core.DTO_s.Inventory;

public class CreateInventoryMovementDto
{
    public int ProductVariantId { get; set; }
    public int? BatchId { get; set; }
    public int MovementTypeId { get; set; }
    public int Quantity { get; set; }
    public string? ReferenceType { get; set; }
    public int? ReferenceId { get; set; }
    public string? Notes { get; set; }
}
