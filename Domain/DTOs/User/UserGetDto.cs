using Domain.DTOs.Events;

namespace Domain.DTOs.User;

public class UserGetDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public List<EventDto> LikedEvents { get; set; }
    
    public List<PosterDto> LikedPosters { get; set; }
}