namespace Core.Entities;

/// <summary>
/// Trazabilidad completa de cambios de estado del pedido. Inmutable. orders.order_status_id siempre refleja el estado actual.
/// </summary>
public partial class OrderStatusHistory
{
    public int history_id { get; set; }

    public int order_id { get; set; }

    public int order_status_id { get; set; }

    /// <summary>
    /// Usuario que realizó el cambio. NULL si fue un proceso automático del sistema.
    /// </summary>
    public int? changed_by_user_id { get; set; }

    public string? notes { get; set; }

    public DateTime created_at { get; set; }

    public virtual User? changed_by_user { get; set; }

    public virtual Order order { get; set; } = null!;

    public virtual OrderStatus order_status { get; set; } = null!;
}
