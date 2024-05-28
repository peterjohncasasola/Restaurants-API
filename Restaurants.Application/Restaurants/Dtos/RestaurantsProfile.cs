
using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
            {
                PostalCode = src.PostalCode,
                City = src.City,
                Street = src.Street,
            }));

        CreateMap<Restaurant, UpdateRestaurantCommand>()
        .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address != null ? src.Address.PostalCode : null))
        .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address != null ? src.Address.City : null))
        .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address != null ? src.Address.Street : null));


        CreateMap<UpdateRestaurantCommand, Restaurant>()
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
