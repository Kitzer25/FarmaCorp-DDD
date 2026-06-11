namespace Core.Entities;

public partial class VExpiringBatch
{
    public int? batch_id { get; set; }

    public string? batch_number { get; set; }

    public DateOnly? expiration_date { get; set; }

    public int? days_until_expiry { get; set; }

    public int? current_quantity { get; set; }

    public string? sku { get; set; }

    public string? product_name { get; set; }

    public string? drug_form { get; set; }
}
