namespace Core.Entities;

/// <summary>
/// Tabla puente N:M entre users y roles. Un usuario puede tener múltiples roles. Registra quién asignó el rol.
/// </summary>
public partial class UserRole
{
    public int user_id { get; set; }

    public int role_id { get; set; }

    public DateTime assigned_at { get; set; }

    public int? assigned_by_user_id { get; set; }

    public virtual User? assigned_by_user { get; set; }

    public virtual Role role { get; set; } = null!;

    public virtual User user { get; set; } = null!;
}
