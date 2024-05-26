using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Extensions
{
    public static partial class ModelBuilderExtensions
    {
        public static void ConfigureOwnedTypeNavigationsAsRequired(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.IsOwned())
                {
                    var ownership = entityType.FindOwnership();

                    if (ownership is null)
                        return;

                    if (ownership.IsUnique)
                    {
                        ownership.IsRequiredDependent = false;
                    }
                }
            }
        }
    }
}
