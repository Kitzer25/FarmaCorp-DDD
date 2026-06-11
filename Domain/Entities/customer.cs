namespace Core.Entities;

/// <summary>
/// Compradores del eCommerce. Separados de users (personal interno) por tener flujos, permisos y datos distintos.
/// </summary>
public partial class Customer
{
    public int customer_id { get; set; }

    public string email { get; set; } = null!;

    public string password_hash { get; set; } = null!;

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string? phone { get; set; }

    public DateOnly? date_of_birth { get; set; }

    /// <summary>
    /// DNI, RUC, pasaporte u otro documento de identidad.
    /// </summary>
    public string? document_number { get; set; }

    public bool is_email_verified { get; set; }

    public bool is_active { get; set; }

    public DateTime? last_login_at { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }

    /// <summary>
    /// Soft delete. Preserva historial de pedidos del cliente.
    /// </summary>
    public DateTime? deleted_at { get; set; }

    public virtual ICollection<AuditLog> audit_logs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Cart> carts { get; set; } = new List<Cart>();

    public virtual ICollection<CustomerAddress> customer_addresses { get; set; } = new List<CustomerAddress>();

    public virtual ICollection<CustomerWishlist> customer_wishlists { get; set; } = new List<CustomerWishlist>();

    public virtual ICollection<Order> orders { get; set; } = new List<Order>();

    public virtual ICollection<PrescriptionUpload> prescription_uploads { get; set; } = new List<PrescriptionUpload>();
}
