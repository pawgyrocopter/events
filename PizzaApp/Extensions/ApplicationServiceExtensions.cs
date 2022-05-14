using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;

namespace PizzaApp.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        return services;
    }
}