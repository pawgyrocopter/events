using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public string Adress { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}