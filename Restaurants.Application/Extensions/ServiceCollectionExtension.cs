using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Validators;
namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtension).Assembly;

            services.AddScoped<IRestaurantsService, RestaurantsService>();

            services.AddScoped<CreateRestaurantDtoValidator>();

            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly).AddFluentValidationAutoValidation();
        }
    }
}
