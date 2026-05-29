namespace Core.Entities;

/// <summary>
/// Personal interno: administradores, farmacéuticos, vendedores. Separado de customers. Accede al backoffice, no al eCommerce público.
/// </summary>
public partial class User
{
    public int user_id { get; set; }

    public string email { get; set; } = null!;

    public string password_hash { get; set; } = null!;

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string? phone { get; set; }

    public bool is_active { get; set; }

    public DateTime? last_login_at { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public virtual ICollection<AuditLog> audit_logs { get; set; } = new List<AuditLog>();

    public virtual ICollection<InventoryMovement> inventory_movements { get; set; } = new List<InventoryMovement>();

    public virtual ICollection<OrderStatusHistory> order_status_histories { get; set; } = new List<OrderStatusHistory>();

    public virtual ICollection<PrescriptionUpload> prescription_uploads { get; set; } = new List<PrescriptionUpload>();

    public virtual ICollection<UserRole> user_roleassigned_by_users { get; set; } = new List<UserRole>();

    public virtual ICollection<UserRole> user_roleusers { get; set; } = new List<UserRole>();
}
