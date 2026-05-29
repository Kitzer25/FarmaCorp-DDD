namespace Core.Entities;

/// <summary>
/// Líneas del carrito. Una por variante. El precio se recalcula al hacer checkout; unit_price_snapshot es solo referencia.
/// </summary>
public partial class CartItem
{
    public int cart_item_id { get; set; }

    public int cart_id { get; set; }

    public int product_variant_id { get; set; }

    public int quantity { get; set; }

    /// <summary>
    /// Precio al momento de agregar al carrito. El precio real de venta se toma de product_variants.price al confirmar.
    /// </summary>
    public decimal? unit_price_snapshot { get; set; }

    public DateTime added_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual Cart cart { get; set; } = null!;

    public virtual ProductVariant product_variant { get; set; } = null!;
}
