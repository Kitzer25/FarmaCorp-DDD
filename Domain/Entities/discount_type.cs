namespace Core.Entities;

/// <summary>
/// Determina cómo interpretar el campo discount_value en promotions. Seed: Percentage, FixedAmount.
/// </summary>
public partial class DiscountType
{
    public int discount_type_id { get; set; }

    public string name { get; set; } = null!;

    public virtual ICollection<Promotion> promotions { get; set; } = new List<Promotion>();
}
