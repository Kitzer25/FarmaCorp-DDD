namespace Core.Entities;

/// <summary>
/// Roles para control de acceso del personal interno. Un usuario puede tener múltiples roles via user_roles.
/// </summary>
public partial class Role
{
    public int role_id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public bool is_active { get; set; }

    public virtual ICollection<UserRole> user_roles { get; set; } = new List<UserRole>();
}
