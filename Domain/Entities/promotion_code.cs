namespace Core.Entities;

/// <summary>
/// Códigos de cupón asociados a una promoción. Ej: VERANO10. Tienen control de usos propio además del límite de la promoción.
/// </summary>
public partial class PromotionCode
{
    public int code_id { get; set; }

    public int promotion_id { get; set; }

    /// <summary>
    /// Código que ingresa el cliente al hacer checkout. Único en todo el sistema.
    /// </summary>
    public string code { get; set; } = null!;

    public int? max_uses { get; set; }

    public int current_uses { get; set; }

    public bool is_active { get; set; }

    public DateTime created_at { get; set; }

    public virtual ICollection<Order> orders { get; set; } = new List<Order>();

    public virtual Promotion promotion { get; set; } = null!;
}
