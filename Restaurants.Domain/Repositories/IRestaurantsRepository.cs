using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        IQueryable<Restaurant> GetQuery();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant> Create(Restaurant entity);
        Task<bool> Update(Restaurant entity);
        Task<int> SaveChangesAsync();
        Task Delete(Restaurant restaurant);
    }
}
