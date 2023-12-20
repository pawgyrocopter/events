using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var roles = new List<Role>()
        {
            new() {Id = Guid.NewGuid(), Name = "Customer"},
            new() {Id = Guid.NewGuid(), Name = "Admin"}
        };

        foreach (var role in roles)
        {
            Console.WriteLine(role.Name + "created");
            await roleManager.CreateAsync(role);
        }
        
        // for (int i = 0; i < 10; i++)
        // {
        //     var user = new User() {Id = i + 1, UserName = $"user{i + 1}", Email = $"user{i + 1}@gmail.com"};
        //     await userManager.CreateAsync(user, "123123");
        //     await userManager.AddToRoleAsync(user, "Customer");
        // }

        var admin = new User()
        {
            Id = Guid.NewGuid(),
            UserName = "admin"
        };
        
        await userManager.CreateAsync(admin, "123123");
        await userManager.AddToRolesAsync(admin, new[] {"Admin", "Customer"});
    }
}