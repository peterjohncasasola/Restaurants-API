using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;


namespace Restaurants.Infrastructure.Configurations
{
    internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurants");
            builder.HasKey(p => p.Id);
            builder.OwnsOne(p => p.Address);

            builder.Navigation(p => p.Address).IsRequired(false);

            builder.HasMany(p => p.Dishes).WithOne().HasForeignKey(p => p.RestaurantId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).IsRequired();


        }
    }
}
