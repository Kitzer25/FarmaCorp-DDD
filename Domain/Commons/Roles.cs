namespace Core.Constants;

/// <summary>
/// Nombres de roles del sistema.
/// Deben coincidir con los valores de la tabla <c>roles</c> en base de datos.
/// </summary>
public static class Roles
{
    public const string Admin      = "Admin";
    public const string Pharmacist = "Pharmacist";
    public const string SalesRep   = "SalesRep";
    public const string Manager    = "Manager";
    public const string Client     = "Client";
}