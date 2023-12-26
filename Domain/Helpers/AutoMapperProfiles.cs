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
        CreateMap<User, UserGetDto>()
            .ForMember(d => d.Id, e => e.MapFrom(x => x.Id))
            .ForMember(d => d.UserName, e => e.MapFrom(x => x.UserName))
            .ForMember(d => d.Email, e => e.MapFrom(x => x.Email))
            .ForMember(d => d.LikedEvents, e => e.MapFrom(x => x.LikedEvents))
            .ForMember(d => d.LikedPosters, e => e.MapFrom(x => x.LikedPosters))
            .ForMember(d => d.Events, e => e.MapFrom(x => x.Events));
        
        CreateMap<User, UserCreatorDto>()
            .ForMember(d => d.Base64Photo, e 
                => e.MapFrom(x => x.Base64Photo))
            .ForMember(d => d.Name, e 
                => e.MapFrom(x => x.UserName))
            .ForMember(d => d.Id, e 
                => e.MapFrom(x => x.Id))
            .ForMember(d => d.Email, e 
                => e.MapFrom(x => x.Email));
        
        CreateMap<Event, EventDto>()
            .ForMember(d => d.Users, e => e.MapFrom(x => x.Users))
            .ForMember(d => d.PosterId, e => e.MapFrom(x => x.PosterId));

        CreateMap<Poster, PosterDto>()
            .ForMember(d => d.Base64Photo, e 
                => e.MapFrom(x => x.Base64Photo))
            .ForMember(d => d.Id, e 
            => e.MapFrom(x => x.Id));
    }
}