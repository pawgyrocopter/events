using Domain.DTOs.Events;

namespace Domain.DTOs.User;

public class UserGetDto
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; }
    
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string ShortDescription { get; set; }
    public string VKLink { get; set; }
    public string TelegramLink { get; set; }
    public int Age { get; set; }
    public string Interests { get; set; }

    
    public string Email { get; set; }
    
    public List<EventDto> Events { get; set; }

    public List<EventDto> LikedEvents { get; set; }
    
    public List<PosterDto> LikedPosters { get; set; }
}