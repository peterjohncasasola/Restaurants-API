﻿
using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
            {
                PostalCode = src.PostalCode,
                City = src.City,
                Street = src.Street,
            }));

        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
    }
}
