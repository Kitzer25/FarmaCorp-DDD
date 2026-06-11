namespace Core.Entities;

/// <summary>
/// Fabricantes de medicamentos. Un laboratorio puede fabricar múltiples productos.
/// </summary>
public partial class Laboratory
{
    public int laboratory_id { get; set; }

    public string name { get; set; } = null!;

    public string? country_of_origin { get; set; }

    public string? contact_email { get; set; }

    public string? phone { get; set; }

    public string? website { get; set; }

    public bool is_active { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<ProductBatch> product_batches { get; set; } = new List<ProductBatch>();

    public virtual ICollection<Product> products { get; set; } = new List<Product>();
}
