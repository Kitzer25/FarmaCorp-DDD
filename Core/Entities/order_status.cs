namespace Core.Entities;

/// <summary>
/// Estados del ciclo de vida de un pedido. El historial de cambios se guarda en order_status_history.
/// </summary>
public partial class OrderStatus
{
    public int order_status_id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public int sort_order { get; set; }

    public bool is_active { get; set; }

    public virtual ICollection<OrderStatusHistory> order_status_histories { get; set; } = new List<OrderStatusHistory>();

    public virtual ICollection<Order> orders { get; set; } = new List<Order>();
}
