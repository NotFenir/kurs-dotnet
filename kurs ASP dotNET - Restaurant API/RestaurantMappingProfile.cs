using AutoMapper;
using kurs_ASP_dotNET___Restaurant_API.Controllers;
using kurs_ASP_dotNET___Restaurant_API.Entities;
using kurs_ASP_dotNET___Restaurant_API.Models;

namespace kurs_ASP_dotNET___Restaurant_API;

public class RestaurantMappingProfile : Profile
{
    public RestaurantMappingProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(m => m.City,
                c => c.MapFrom(s => s.Address!.City))
            .ForMember(m => m.Street, 
                c => c.MapFrom(s => s.Address!.Street))
            .ForMember(m => m.PostalCode, 
                c => c.MapFrom(s => s.Address!.PostalCode));

        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(r => r.Address,
                c => c.MapFrom(dto => new Address()
                    { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));

        CreateMap<UpdateRestaurantDto, Restaurant>();
        
        CreateMap<Dish, DishDto>();
        CreateMap<CreateDishDto, Dish>();
        CreateMap<UpdateDishDto, Dish>()
            .ForMember(d => d.Name,
                opt => opt.Condition(src => src.Name != null))
            .ForMember(d => d.Description,
            opt => opt.Condition(src => src.Description != null))
            .ForMember(d => d.Price,
            opt => opt.Condition(src => src.Price != default(decimal)));
    }
}