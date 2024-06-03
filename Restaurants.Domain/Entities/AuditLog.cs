namespace Restaurants.Domain.Entities;

public class AuditLog
{
    public long Id { get; set; }
    public required string EntityName { get; set; }
    public required string Action { get; set; }
    public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    public string? UserEmail { get; set; }
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public required string Changes { get; set; }
    public string? ChangedColumns { get; set; }
    public string NewValues { get; set; } = default!;
    public string? OldValues { get; set; }
    public string KeyValues { get; set; } = default!;
    public string TableName { get; set; } = default!;

}
