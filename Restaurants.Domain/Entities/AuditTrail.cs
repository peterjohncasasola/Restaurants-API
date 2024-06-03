namespace Restaurants.Domain.Entities
{
    public class AuditTrail : IAuditTrail
    {
        public int? CreatedByUserId { get; set; }
        public int? LastUpdatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedOn { get; set; }
    }

    public interface IAuditTrail
    {
        public int? CreatedByUserId { get; set; }
        public int? LastUpdatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

    }
}
