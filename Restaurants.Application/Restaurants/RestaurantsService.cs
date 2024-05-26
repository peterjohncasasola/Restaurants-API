using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService
    (
        IRestaurantsRepository restaurantsRepository,
        ILogger<RestaurantsService> logger,
        IMapper mapper
    ) : IRestaurantsService
    
    {
        public async Task<RestaurantDto> Create(CreateRestaurantDto dto)
        {
            logger.LogInformation("Creating restaurant");

            var restaurant = mapper.Map<Restaurant>(dto);
            var createdRestaurant = await restaurantsRepository.Create(restaurant);

            var restaurantDto = mapper.Map<RestaurantDto>(createdRestaurant);

            return restaurantDto;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAll()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();

            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantsDtos!;
        }

        public async Task<RestaurantDto?> GetById(int id)
        {
            logger.LogInformation($"Getting the restaurant with ID of {id}");

            var restaurant = await restaurantsRepository.GetByIdAsync(id);

            var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);

            return restaurantDto;

        }
    }
    
}
