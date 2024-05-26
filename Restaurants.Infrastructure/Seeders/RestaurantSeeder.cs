using Restaurants.Infrastructure.Persistence;
using Restaurants.Domain.Entities;
using Bogus;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants() 
        {
            var faker = new Faker();

            var restaurants = new Faker<Restaurant>()
            .RuleFor(r => r.Name, f => f.Commerce.Department(1))
            .RuleFor(r => r.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(r => r.ContactEmail, f => f.Internet.ExampleEmail())
            .RuleFor(r => r.ContactNumber, f => f.Phone.PhoneNumber())
            .RuleFor(r => r.Description, f => f.Commerce.Department(1))
            .RuleFor(r => r.HasDelivery, true);

            var dishes = new Faker<Dish>()
                            .RuleFor(d => d.Name, f => f.Commerce.ProductName())
                            .RuleFor(d => d.Description, f => f.Commerce.ProductDescription())
                            .RuleFor(d => d.Price, f => f.Random.Decimal(1.00M, 1000.00M));

            var address = new Faker<Address>()
            .RuleFor(p => p.City, f => f.Address.City())
            .RuleFor(p => p.Street, f => f.Address.StreetAddress())
            .RuleFor(p => p.PostalCode, f => f.Address.ZipCode());

            var generatedRestaurants = restaurants.Generate(20);


            foreach(var restaurant in generatedRestaurants)
            {
                restaurant.Address = address.Generate(1)[0];
                restaurant.Dishes.AddRange(dishes.Generate(faker.Random.Number(1, 10)));
            }

            return generatedRestaurants;
        }
    }
}
