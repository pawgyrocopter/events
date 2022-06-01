using AutoMapper;
using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Helpers;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Toping, TopingDto>();
        CreateMap<Pizza, PizzaDto>()
            .ForMember(d => d.PhotoUrl,
                e => e.MapFrom(src => src.Photo.Url));
        
        CreateMap<Order, OrderDto>()
            .ForMember(d => d.Pizzas, e => 
                e.MapFrom(src => src.Pizzas))
            .ForMember(d => d.Name, e=> 
                e.MapFrom(src => src.User.UserName))
            .ForMember(d => d.OrderId,
                e => e.MapFrom(src => src.Id));
        
        CreateMap<PizzaOrder, PizzaDto>()
            .ForMember(d => d.Name, e =>
                e.MapFrom(src => src.Pizza.Name))
            .ForMember(d => d.Ingredients, e =>
                e.MapFrom(src => src.Pizza.Ingredients))
            .ForMember(d => d.Cost, e =>
                e.MapFrom(src => src.Pizza.Cost))
            .ForMember(d => d.Weight, e =>
                e.MapFrom(src => src.Pizza.Weight))
            .ForMember(d => d.PhotoUrl, e =>
                e.MapFrom(src => src.Pizza.Photo.Url))
            .ForMember(d => d.Topings, e =>
                e.MapFrom(src => src.Topings));

        CreateMap<TopingOrder, TopingDto>()
            .ForMember(d => d.Name, e =>
                e.MapFrom(src => src.Toping.Name))
            .ForMember(d => d.Counter, e =>
                e.MapFrom(src => src.Counter))
            .ForMember(d => d.Id, e =>
                e.MapFrom(src => src.Toping.Id));
    }
}