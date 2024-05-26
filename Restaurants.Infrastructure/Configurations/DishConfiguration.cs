using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Restaurants.Infrastructure.Configurations
{
    internal class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Price).HasColumnType("decimal").HasPrecision(18, 2);
            builder.Property(p => p.KiloCalories).IsRequired(false);
        }
    }
}
