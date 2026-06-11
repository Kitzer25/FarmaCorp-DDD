namespace Core.Entities;

/// <summary>
/// Presentación específica y vendible de un medicamento. Ej: Paracetamol 500mg x30 tabletas.
/// </summary>
public partial class ProductVariant
{
    public int product_variant_id { get; set; }

    public int product_id { get; set; }

    public int drug_form_id { get; set; }

    public int? unit_id { get; set; }

    /// <summary>
    /// Valor numérico de la concentración. La unidad se define en unit_id.
    /// </summary>
    public decimal? concentration { get; set; }

    /// <summary>
    /// Unidades por empaque. Ej: 30 (tabletas por caja).
    /// </summary>
    public int package_size { get; set; }

    public string? package_description { get; set; }

    public string sku { get; set; } = null!;

    public string? barcode { get; set; }

    public decimal price { get; set; }

    /// <summary>
    /// Precio original antes del descuento. Se muestra tachado en el eCommerce.
    /// </summary>
    public decimal? compare_at_price { get; set; }

    public int sort_order { get; set; }

    public bool is_active { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }

    /// <summary>
    /// Soft delete. Las variantes no se borran para preservar historial de pedidos.
    /// </summary>
    public DateTime? deleted_at { get; set; }

    public virtual ICollection<CartItem> cart_items { get; set; } = new List<CartItem>();

    public virtual ICollection<CustomerWishlist> customer_wishlists { get; set; } = new List<CustomerWishlist>();

    public virtual DrugForm drug_form { get; set; } = null!;

    public virtual Inventory? inventory { get; set; }

    public virtual ICollection<InventoryMovement> inventory_movements { get; set; } = new List<InventoryMovement>();

    public virtual ICollection<OrderItem> order_items { get; set; } = new List<OrderItem>();

    public virtual Product product { get; set; } = null!;

    public virtual ICollection<ProductBatch> product_batches { get; set; } = new List<ProductBatch>();

    public virtual ICollection<ProductImage> product_images { get; set; } = new List<ProductImage>();

    public virtual MeasurementUnit? unit { get; set; }

    public virtual ICollection<Promotion> promotions { get; set; } = new List<Promotion>();
}
