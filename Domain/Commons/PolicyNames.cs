namespace Domain.Commons;

/// <summary>
/// Nombres de las políticas de autorización registradas en Program.cs.
/// Evita cadenas mágicas en los atributos [Authorize(Policy = "...")].
/// </summary>
public static class PolicyNames
{
    /// <summary>Solo Admin.</summary>
    public const string AdminOnly        = "AdminOnly";

    /// <summary>Admin o Pharmacist — recetas, inventario.</summary>
    public const string PharmacistAccess = "PharmacistAccess";

    /// <summary>Admin, SalesRep o Manager — productos, pedidos, lotes.</summary>
    public const string SalesAccess      = "SalesAccess";

    /// <summary>Admin o Manager — reportes, clientes, promociones.</summary>
    public const string ManagerAccess    = "ManagerAccess";

    /// <summary>Cualquier empleado interno.</summary>
    public const string InternalAccess   = "InternalAccess";

    /// <summary>Solo Client (comprador del eCommerce).</summary>
    public const string ClientAccess     = "ClientAccess";

    // ────────────────────────────────────────────────────────
    // Alias de retrocompatibilidad — no rompe controladores
    // ya implementados por otros miembros del equipo.
    // ────────────────────────────────────────────────────────
    /// <summary>Alias de AdminOnly. Conservado por retrocompatibilidad.</summary>
    public const string TotalAccess      = "TotalAccess";
}