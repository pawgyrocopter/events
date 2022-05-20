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
        
    }
}