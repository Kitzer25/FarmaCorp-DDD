namespace Core.Entities;

/// <summary>
/// Métodos de pago habilitados. is_online diferencia pagos digitales de presenciales.
/// </summary>
public partial class PaymentMethod
{
    public int payment_method_id { get; set; }

    public string name { get; set; } = null!;

    public bool is_online { get; set; }

    public bool is_active { get; set; }

    public virtual ICollection<OrderPayment> order_payments { get; set; } = new List<OrderPayment>();
}
