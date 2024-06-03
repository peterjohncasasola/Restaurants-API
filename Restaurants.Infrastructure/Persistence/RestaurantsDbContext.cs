using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Helpers;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options, IUserContext userService) : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }
    internal DbSet<AuditLog> AuditLogs { get; set; } = default!;

    readonly CurrentUser? currentUser = userService.GetCurrentUser();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantsDbContext).Assembly);
        modelBuilder.ConfigureOwnedTypeNavigationsAsRequired();
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditEntries = new List<AuditEntry>();

        var modifiedEntities = ChangeTracker.Entries()
                               .Where(e => e.State == EntityState.Added
                                || e.State == EntityState.Modified
                                || e.State == EntityState.Deleted)
                                .ToList();

        if (!modifiedEntities.Any()) return 0;

        if (currentUser == null)
            return await base.SaveChangesAsync(cancellationToken);

        foreach (var modifiedEntity in modifiedEntities)
        {
            var auditEntry = new AuditEntry(modifiedEntity, currentUser);
            auditEntries.Add(auditEntry);
        }

        var logs = auditEntries.Select(x => x.ToAudit());
        AuditLogs.AddRange(logs);

        return await base.SaveChangesAsync(cancellationToken);
    }

}

 