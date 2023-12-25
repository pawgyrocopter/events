using Domain.DTOs.User;

namespace Domain.DTOs.Events;

public record EventUpdateDto
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Date { get; set; }
    
    public List<UserEventUpdateDto> UsersToAdd { get; set; }
    
    public List<UserEventUpdateDto> UsersToRemove { get; set; }
}