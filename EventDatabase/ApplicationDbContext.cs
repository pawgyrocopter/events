using EventDatabase.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EventDatabase;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<EventEntity> Events { get; set; }

    public string DbPath => @"C:Users\kR9_h\Desktop\МЗИ\EventApi\app.db";

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}