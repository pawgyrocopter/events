using Microsoft.AspNetCore.Identity;

namespace PizzaApp.Entities;

public class UserRole : IdentityUserRole<int>
{
    public Role Role { get; set; }
    public User User { get; set; }
}