using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Interfaces;
using PizzaApp.Services;

namespace PizzaApp.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        return services;
    }
}