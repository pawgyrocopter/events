using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public List<Event> Events { get; set; }
    
    public List<UserRole> UserRoles { get; set; }
}