namespace Core.Entities;

/// <summary>
/// Tabla puente N:M. Lista de deseos: cada cliente puede guardar múltiples variantes, y cada variante puede aparecer en múltiples listas.
/// </summary>
public partial class CustomerWishlist
{
    public int customer_id { get; set; }

    public int product_variant_id { get; set; }

    public DateTime added_at { get; set; }

    public virtual Customer customer { get; set; } = null!;

    public virtual ProductVariant product_variant { get; set; } = null!;
}
