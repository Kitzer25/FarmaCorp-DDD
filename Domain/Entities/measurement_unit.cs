namespace Core.Entities;

/// <summary>
/// Unidades de concentración y volumen. Seed: mg, ml, g, mcg, UI, %.
/// </summary>
public partial class MeasurementUnit
{
    public int unit_id { get; set; }

    public string name { get; set; } = null!;

    public string symbol { get; set; } = null!;

    public bool is_active { get; set; }

    public virtual ICollection<ProductVariant> product_variants { get; set; } = new List<ProductVariant>();
}
