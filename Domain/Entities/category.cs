namespace Core.Entities;

/// <summary>
/// Clasificación jerárquica de medicamentos. Soporta subcategorías via auto-referencia.
/// </summary>
public partial class Category
{
    public int category_id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    /// <summary>
    /// NULL = categoría raíz. Permite un nivel de subcategorías.
    /// </summary>
    public int? parent_category_id { get; set; }

    /// <summary>
    /// Identificador URL-friendly único. Ej: analgesicos, antibioticos.
    /// </summary>
    public string slug { get; set; } = null!;

    public int sort_order { get; set; }

    public bool is_active { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<Category> Inverseparent_category { get; set; } = new List<Category>();

    public virtual Category? parent_category { get; set; }

    public virtual ICollection<ProductCategory> product_categories { get; set; } = new List<ProductCategory>();

    public virtual ICollection<Product> products { get; set; } = new List<Product>();
}
