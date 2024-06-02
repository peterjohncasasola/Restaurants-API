﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler 
(
    ILogger<UpdateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository
) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating the restaurant with ID of {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
            return false;

        mapper.Map(request, restaurant);

        var result = await restaurantsRepository.SaveChangesAsync();

        if (result > 0)
            return true;
        return false;
    }
}
