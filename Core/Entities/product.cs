namespace Core.Entities;

/// <summary>
/// Medicamento como concepto abstracto. No se vende directamente; las variantes (product_variants) son las unidades vendibles.
/// </summary>
public partial class Product
{
    public int product_id { get; set; }

    public string name { get; set; } = null!;

    /// <summary>
    /// Denominación Común Internacional (DCI).
    /// </summary>
    public string? generic_name { get; set; }

    public string? description { get; set; }

    public string? short_description { get; set; }

    public int category_id { get; set; }

    public int laboratory_id { get; set; }

    /// <summary>
    /// Si TRUE, el pedido requiere receta médica verificada antes de despachar.
    /// </summary>
    public bool requires_prescription { get; set; }

    /// <summary>
    /// Sustancia controlada. Requiere restricciones adicionales de venta.
    /// </summary>
    public bool is_controlled { get; set; }

    public string? active_ingredient { get; set; }

    /// <summary>
    /// URL amigable única. Ej: paracetamol-genfar.
    /// </summary>
    public string slug { get; set; } = null!;

    /// <summary>
    /// Palabras clave separadas por coma para búsqueda interna.
    /// </summary>
    public string? tags { get; set; }

    public bool is_active { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }

    /// <summary>
    /// Soft delete. NULL = activo. Filtrar siempre con deleted_at IS NULL.
    /// </summary>
    public DateTime? deleted_at { get; set; }

    public virtual Category category { get; set; } = null!;

    public virtual Laboratory laboratory { get; set; } = null!;

    public virtual ICollection<ProductCategory> product_categories { get; set; } = new List<ProductCategory>();

    public virtual ICollection<ProductImage> product_images { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductVariant> product_variants { get; set; } = new List<ProductVariant>();
}
