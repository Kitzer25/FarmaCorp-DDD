namespace Core.Entities;

public partial class VCustomerOrderSummary
{
    public int? customer_id { get; set; }

    public string? email { get; set; }

    public string? full_name { get; set; }

    public long? total_orders { get; set; }

    public decimal? total_spent { get; set; }

    public DateTime? last_order_at { get; set; }
}
