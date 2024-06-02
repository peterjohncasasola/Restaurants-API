using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler 
(
    ILogger<CreateRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository
) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant with id : {RestaurantId}", request.Id); 

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        await restaurantsRepository.Delete(restaurant);
        return true;
    }
}
