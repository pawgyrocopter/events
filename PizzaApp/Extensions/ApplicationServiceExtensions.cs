using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Entities;
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
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPizzaRepository, PizzaRepository>();
        services.AddScoped<ITopingRepository, TopingRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddSignalR();
        // services.AddSingleton<StateCheckerService>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        return services;
    }
}