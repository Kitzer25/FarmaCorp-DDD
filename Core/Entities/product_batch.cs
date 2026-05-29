namespace Core.Entities;

/// <summary>
/// Lotes de medicamentos con trazabilidad de vencimiento. Obligatorio para regulación farmacéutica. Lógica FEFO aplicada al despachar.
/// </summary>
public partial class ProductBatch
{
    public int batch_id { get; set; }

    public int product_variant_id { get; set; }

    public int? laboratory_id { get; set; }

    /// <summary>
    /// Número de lote impreso en el empaque del fabricante.
    /// </summary>
    public string batch_number { get; set; } = null!;

    public DateOnly? manufacture_date { get; set; }

    /// <summary>
    /// Fecha de vencimiento. Nunca vender si expiration_date &lt; NOW().
    /// </summary>
    public DateOnly expiration_date { get; set; }

    public int initial_quantity { get; set; }

    public int current_quantity { get; set; }

    public string? notes { get; set; }

    public bool is_active { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<InventoryMovement> inventory_movements { get; set; } = new List<InventoryMovement>();

    public virtual Laboratory? laboratory { get; set; }

    public virtual ICollection<OrderItem> order_items { get; set; } = new List<OrderItem>();

    public virtual ProductVariant product_variant { get; set; } = null!;
}
