using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ApplicationDbContext = EventDatabase.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

string connectionString = builder.Configuration.GetConnectionString("SqliteConnection");

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString, b => b.MigrationsAssembly("EventApi")));
builder.Services.AddScoped<ApplicationDbContext>(sp =>
    sp.GetService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 0;
    options.Password.RequiredUniqueChars = 0;
    
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    
    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapSwagger().RequireAuthorization();
app.MapIdentityApi<IdentityUser>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();