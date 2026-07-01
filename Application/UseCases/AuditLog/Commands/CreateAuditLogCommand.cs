namespace Application.UseCases.AuditLog.Commands;

public class CreateAuditLogCommand
{
    private string TableName;

    private string RecordId;

    private string Action;

    private string? OldValues = null;

    private string? NewValues = null;

    private int? UserId = null;

    private int? CustomerId = null;

    private string? IpAddress = null;

    private string? UserAgent = null;

}