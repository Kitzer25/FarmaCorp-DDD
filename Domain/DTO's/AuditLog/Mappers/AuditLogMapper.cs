namespace Domain.DTO_s.AuditLog.Mappers;

public static class AuditLogMapper
{
    public static AuditLogDto ToDto(this Entities.AuditLog log)
    {
        return new AuditLogDto
        {
            AuditLogId = log.audit_log_id,
            TableName = log.table_name,
            RecordId = log.record_id,
            Action = log.action,
            OldValues = log.old_values,
            NewValues = log.new_values,
            UserId = log.user_id,
            CustomerId = log.customer_id,
            IpAddress = log.ip_address,
            UserAgent = log.user_agent,
            CreatedAt = log.created_at
        };
    }

    public static Entities.AuditLog ToEntity(
        string tableName,
        string recordId,
        string action,
        string? oldValuesJson,
        string? newValuesJson,
        int? userId,
        int? customerId)
    {
        return new Entities.AuditLog
        {
            table_name = tableName,
            record_id = recordId,
            action = action,
            old_values = oldValuesJson,
            new_values = newValuesJson,
            user_id = userId,
            customer_id = customerId,
            created_at = DateTime.UtcNow
        };
    }
}