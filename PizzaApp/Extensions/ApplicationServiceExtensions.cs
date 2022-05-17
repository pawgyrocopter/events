using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Helpers;
using PizzaApp.Interfaces;
using PizzaApp.Services;

namespace PizzaApp.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddScoped<IPhotoService, PhotoService>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        return services;
    }
}