namespace Core.Entities;

/// <summary>
/// Cabecera del pedido. Documento contable inmutable. Los montos son snapshot al momento de confirmar y NUNCA se recalculan.
/// </summary>
public partial class Order
{
    public int order_id { get; set; }

    /// <summary>
    /// Identificador legible para el cliente. Ej: ORD-2024-000001. Generado por la aplicación con una secuencia.
    /// </summary>
    public string order_number { get; set; } = null!;

    public int customer_id { get; set; }

    public int order_status_id { get; set; }

    public int shipping_address_id { get; set; }

    public int? promotion_code_id { get; set; }

    public decimal subtotal { get; set; }

    public decimal tax_amount { get; set; }

    public decimal shipping_cost { get; set; }

    public decimal discount_amount { get; set; }

    /// <summary>
    /// subtotal + tax_amount + shipping_cost - discount_amount. Calculado y fijado al confirmar el pedido.
    /// </summary>
    public decimal total { get; set; }

    public string? customer_notes { get; set; }

    public string? internal_notes { get; set; }

    public DateOnly? estimated_delivery { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual Customer customer { get; set; } = null!;

    public virtual ICollection<OrderItem> order_items { get; set; } = new List<OrderItem>();

    public virtual ICollection<OrderPayment> order_payments { get; set; } = new List<OrderPayment>();

    public virtual OrderStatus order_status { get; set; } = null!;

    public virtual ICollection<OrderStatusHistory> order_status_histories { get; set; } = new List<OrderStatusHistory>();

    public virtual ICollection<PrescriptionUpload> prescription_uploads { get; set; } = new List<PrescriptionUpload>();

    public virtual PromotionCode? promotion_code { get; set; }

    public virtual CustomerAddress shipping_address { get; set; } = null!;
}
