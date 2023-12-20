using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public Role Role { get; set; }
    public User User { get; set; }
}