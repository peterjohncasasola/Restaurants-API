using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories 
{
    internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<Restaurant> Create(Restaurant entity)
        {
            dbContext.Restaurants.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<Restaurant> GetQuery()
        {
            var restaurants = dbContext.Restaurants.AsQueryable();
            return restaurants;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await dbContext.Restaurants.FindAsync(id);
            return restaurant;
        }
    }
}
