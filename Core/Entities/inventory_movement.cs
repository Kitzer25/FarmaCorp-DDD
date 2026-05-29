namespace Core.Entities;

/// <summary>
/// Historial auditado de todos los cambios de stock. Inmutable. Permite reconciliar discrepancias y generar reportes de entradas/salidas.
/// </summary>
public partial class InventoryMovement
{
    public int movement_id { get; set; }

    public int product_variant_id { get; set; }

    public int? batch_id { get; set; }

    public int movement_type_id { get; set; }

    public int? user_id { get; set; }

    /// <summary>
    /// Cantidad siempre positiva. La dirección (entrada/salida) la determina inventory_movement_types.direction.
    /// </summary>
    public int quantity { get; set; }

    /// <summary>
    /// Tipo de la entidad que originó el movimiento.
    /// </summary>
    public string? reference_type { get; set; }

    /// <summary>
    /// ID de la entidad originadora (order_id, batch_id, etc.).
    /// </summary>
    public int? reference_id { get; set; }

    public string? notes { get; set; }

    public DateTime created_at { get; set; }

    public virtual ProductBatch? batch { get; set; }

    public virtual InventoryMovementType movement_type { get; set; } = null!;

    public virtual ProductVariant product_variant { get; set; } = null!;

    public virtual User? user { get; set; }
}
