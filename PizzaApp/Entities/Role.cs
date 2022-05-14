using Microsoft.AspNetCore.Identity;

namespace PizzaApp.Entities;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
}