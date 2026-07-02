namespace Domain.DTO_s.AuditLog;

public class AuditLogDto
{
    public long AuditLogId { get; set; }
    public string TableName { get; set; } = null!;
    public string RecordId { get; set; } = null!;
    public string Action { get; set; } = null!;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public int? UserId { get; set; }
    public int? CustomerId { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime CreatedAt { get; set; }
}
