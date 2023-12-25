using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Events;
using Domain.DTOs.User;
using Domain.Entities;

namespace Domain.Helpers;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserCreatorDto>()
            .ForMember(d => d.Base64Photo, e 
                => e.MapFrom(x => x.Base64Photo))
            .ForMember(d => d.Name, e 
                => e.MapFrom(x => x.UserName))
            .ForMember(d => d.Id, e 
                => e.MapFrom(x => x.Id))
            .ForMember(d => d.Email, e 
                => e.MapFrom(x => x.Email));
        
        CreateMap<Event, EventDto>();
    }
}