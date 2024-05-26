using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        IQueryable<Restaurant> GetQuery();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant> Create(Restaurant entity);
    }
}
