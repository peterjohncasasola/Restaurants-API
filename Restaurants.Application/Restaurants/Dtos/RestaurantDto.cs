using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public Address? Address { get; set; } = null;

        public List<DishDto> Dishes { get; set; } = new();

        public static RestaurantDto? FromEntity(Restaurant? restaurant)
        {
            if (restaurant is null) return null;

            return new RestaurantDto()
            {
                Id = restaurant.Id,
                Address = restaurant.Address,
                Category = restaurant.Category,
                Description = restaurant.Description,
                Name = restaurant.Name,
                HasDelivery = restaurant.HasDelivery
            };
        }
    }
}
