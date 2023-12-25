using Domain.DTOs.User;
using Domain.Entities;

namespace Domain.DTOs.Events;

public record EventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string Description { get; set; }
    
    public DateTime From { get; set; }
    
    public DateTime To { get; set; }
    
    public Guid CreatorId { get; set; }
    
    public UserCreatorDto Creator { get; set; }
    
    public List<UserGetDto> Users { get; set; }
    
    public Guid? PosterId { get; set; }
    
    public EventDto(){}
}