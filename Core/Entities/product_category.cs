namespace Core.Entities;

/// <summary>
/// Tabla puente N:M. Permite asignar un producto a múltiples categorías (Ej: Ibuprofeno → Analgésicos y Antiinflamatorios). is_primary indica la categoría principal para navegación y filtros de catálogo.
/// </summary>
public partial class ProductCategory
{
    public int product_id { get; set; }

    public int category_id { get; set; }

    public bool is_primary { get; set; }

    public DateTime assigned_at { get; set; }

    public virtual Category category { get; set; } = null!;

    public virtual Product product { get; set; } = null!;
}
