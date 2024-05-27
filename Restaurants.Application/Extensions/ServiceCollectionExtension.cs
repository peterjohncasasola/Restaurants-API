using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtension).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

            services.AddScoped<IRestaurantsService, RestaurantsService>();

            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly).AddFluentValidationAutoValidation();
        }
    }
}
