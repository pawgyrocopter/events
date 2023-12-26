using Domain.Entities;
using Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext context)
    {
        if (await userManager.Users.AnyAsync()) 
            return;

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

        List<User> users = new List<User>();
        Random random = new Random();
        string[] firstNames = { "John", "Jane", "Robert", "Emma", "James", "Olivia", "Michael", "Ava", "William", "Isabella" };
        string[] secondNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson" };
        string[] interests = { "Reading", "Traveling", "Cooking", "Music", "Sports", "Art", "Photography", "Writing", "Dancing", "Gaming" };

        for (int i = 1; i <= 10; i++)
        {
            int index = i - 1;
            string firstName = firstNames[index];
            string secondName = secondNames[index];
            string interest = interests[index];

            var newUser = new User
            {
                UserName = firstName + secondName,
                Email = firstName+secondName+"@gmail.com",
                FirstName = firstName,
                SecondName = secondName,
                Age = random.Next(20, 60),
                ShortDescription = $"This is {firstName} {secondName}.",
                Interests = interest,
                TelegramLink = $"https://t.me/{firstName}{secondName}",
                VKLink = $"https://vk.com/{firstName}{secondName}",
                Base64Photo = $"Base64Photo{i}"
            };
            try
            {
                var result1 = await userManager.CreateAsync(newUser, $"user{i + 1}");
                //var user = await userManager.FindByIdAsync(newUser.Id.ToString());
                var result2 = await userManager.AddToRoleAsync(newUser, "Customer");
            }
            catch (Exception ex)
            {
                var a = 1;
            }
           
            users.Add(newUser);
        }

        var admin = new User()
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            Email = "qweqwe"
        };
        
        await userManager.CreateAsync(admin, "123123");
        await userManager.AddToRoleAsync(admin, "Admin");
        
        List<Poster> posters = new List<Poster>();
        string[] posterNames = { "Summer Fest", "Art Expo", "Tech Conference", "Music Concert", "Food Fair", "Book Club", "Film Festival", "Sports Meet", "Charity Run", "Fashion Show" };
        string[] descriptions = { "A fun-filled summer fest", "An expo showcasing local art", "A conference for tech enthusiasts", "A concert featuring popular bands", "A fair for food lovers", "A club for book readers", "A festival for film enthusiasts", "A meet for sports fans", "A run for charity", "A show for fashion enthusiasts" };
        
        string[] photos = Base64Photos.Photos;
        for (int i = 1; i <= 50; i++)
        {
            int index = random.Next(posterNames.Length);
            string posterName = posterNames[index];
            string description = descriptions[index];

            posters.Add(new Poster
            {
                Id = Guid.NewGuid(),
                Name = posterName,
                Description = description,
                From = DateTime.UtcNow.AddDays(-random.Next(1, 10)), // random date from tomorrow to a year from now
                To = DateTime.UtcNow.AddDays(random.Next(1, 20)), // random date from a year and a day from now to two years from now// replace with actual Photo Id
                Base64Photo = photos[random.Next(0, photos.Length - 1)] 
                , // replace with actual Photo object
                Events = new List<Event>() // Add Event objects here
            });
        }

        context.Posters.AddRange(posters);
    }
}