using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public List<Event> Events { get; set; }
    
    public List<Event> LikedEvents { get; set; }
    
    public List<Poster> LikedPosters { get; set; }
    
    public string Base64Photo { get; set; }
    
    public List<Photo> Photos { get; set; }
    
    public List<UserRole> UserRoles { get; set; }
}