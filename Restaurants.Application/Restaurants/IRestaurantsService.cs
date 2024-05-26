using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        public Task<IEnumerable<RestaurantDto>> GetAll();
        public Task<RestaurantDto?> GetById(int id);

    }
}
