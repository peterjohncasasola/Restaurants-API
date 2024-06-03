using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Enums;

namespace Restaurants.Infrastructure.Helpers;

public class AuditEntry
{
    private EntityEntry Entry { get; } = default!;
    private AuditType AuditType { get; set; }
    private string Action { get; set; } = default!;
    private string? AuditUser { get; set; }
    private string TableName { get; set; } = default!;
    private Dictionary<string, object> OldValues { get; } = new();
    private Dictionary<string, object> NewValues { get; } = new();
    private Dictionary<string, object> KeyValues { get; } = new();
    private List<string> ChangedColumns { get; } = new();
    private CurrentUser CurrentUser { get; set; }

    public AuditEntry(EntityEntry entry, CurrentUser currentUser)
    {
        Entry = entry;
        AuditUser = currentUser.Name;
        CurrentUser = currentUser;
        SetChanges();
    }

    private void SetChanges()
    {
        TableName = Entry.Metadata.GetTableName()!;
        
        foreach (var property in Entry.Properties)
        {
            string propertyName = property.Metadata.Name;
            string columName = property.Metadata.GetColumnName();

            if (property.Metadata.IsPrimaryKey())
            {
                KeyValues[propertyName] = property.CurrentValue!;
                continue;
            }

            switch (Entry.State)
            {
                case EntityState.Added:
                    
                    NewValues[propertyName] = property.CurrentValue!;
                    Action = nameof(AuditType.Create);
                    break;

                case EntityState.Deleted:
                    OldValues[propertyName] = property.OriginalValue!;
                    Action = nameof(AuditType.Delete);
                    break;

                case EntityState.Modified:
                    if (property.IsModified)
                    {
                        ChangedColumns.Add(columName);
                        OldValues[propertyName] = property.OriginalValue!;
                        NewValues[propertyName] = property.CurrentValue!;
                        Action = nameof(AuditType.Update);
                    }
                    break;
            }
        }
    }

    public AuditLog ToAudit()
    {
        var auditLog = new AuditLog
        {
            Action = Action,
            ChangedColumns = ChangedColumns.Any() ? JsonConvert.SerializeObject(ChangedColumns) : null,
            EntityName = Entry.Entity.GetType().Name,
            Name = AuditUser,
            UserEmail = CurrentUser.Email,
            UserId = CurrentUser.Id,
            Changes = JsonConvert.SerializeObject(NewValues),
            TimestampUtc = DateTime.UtcNow,
            OldValues = OldValues.Count() > 0 ? JsonConvert.SerializeObject(OldValues) : null,
            KeyValues = JsonConvert.SerializeObject(KeyValues),
            NewValues = JsonConvert.SerializeObject(NewValues),
            TableName = TableName
        };

        return auditLog;

    }
}
