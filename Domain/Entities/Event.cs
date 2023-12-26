using Domain.DTOs.Events;

namespace Domain.Entities;

public class Event
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string Description { get; set; }
    
    public DateTime From { get; set; }
    
    public DateTime To { get; set; }
    
    public Guid CreatorId { get; set; }
    
    public string Base64Photo { get; set; }
    
    public User Creator { get; set; }
    
    public List<User> Users { get; set; }

    public Guid? PosterId { get; set; }
    
    public Poster? Poster { get; set; }
    
    public Event()
    {
        
    }

    public Event(EventDto eventDto)
    {
        Id = Guid.NewGuid();
        Name = eventDto.Name;
        Description = eventDto.Description;
        ShortDescription = eventDto.ShortDescription;
        From = eventDto.From;
        To = eventDto.To;
        Address = eventDto.Address;
        Users = new List<User>();
        CreatorId = eventDto.CreatorId;
    }

    public Event(EventCreateDto eventDto, User user)
    {
        Id = Guid.NewGuid();
        Name = eventDto.Name;
        Description = eventDto.Description;
        ShortDescription = eventDto.ShortDescription;
        From = eventDto.From;   
        To = eventDto.To;
        Address = eventDto.Address;
        Users = new List<User>();
        CreatorId = user.Id;
        PosterId = eventDto.PosterId;
        Base64Photo = eventDto.Base64Photo;
    }
}