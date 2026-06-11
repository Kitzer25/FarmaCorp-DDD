namespace Core.Entities;

/// <summary>
/// Registros de pago por pedido. Permite múltiples intentos y pagos parciales. payment_status maneja el ciclo de vida del pago.
/// </summary>
public partial class OrderPayment
{
    public int payment_id { get; set; }

    public int order_id { get; set; }

    public int payment_method_id { get; set; }

    public decimal amount { get; set; }

    /// <summary>
    /// Referencia externa del pago. Ej: código de operación Yape, número de transferencia bancaria.
    /// </summary>
    public string? transaction_reference { get; set; }

    public string payment_status { get; set; } = null!;

    public DateTime? paid_at { get; set; }

    public string? notes { get; set; }

    public DateTime created_at { get; set; }

    public virtual Order order { get; set; } = null!;

    public virtual PaymentMethod payment_method { get; set; } = null!;
}
