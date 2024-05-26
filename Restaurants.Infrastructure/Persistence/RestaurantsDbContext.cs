using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Configurations;
using Restaurants.Infrastructure.Extensions;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : DbContext(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantsDbContext).Assembly);
        modelBuilder.ConfigureOwnedTypeNavigationsAsRequired();
    }
}

 