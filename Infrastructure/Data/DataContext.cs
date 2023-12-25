using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext<
    User, Role, Guid,
    IdentityUserClaim<Guid>, UserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Event> Events { get; set; }
    
    public DbSet<Poster> Posters { get; set; }
    
    public DbSet<Photo> Photos { get; set; }
    

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
        
        modelBuilder.Entity<Role>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        modelBuilder.Entity<Poster>()
            .HasMany(x => x.Events)
            .WithOne(y => y.Poster);

        modelBuilder.Entity<Event>()
            .HasOne(x => x.Creator)
            .WithMany(x => x.Events);
        
        modelBuilder.Entity<Event>()
            .HasOne(x => x.Poster)
            .WithMany(x => x.Events);
        
        modelBuilder.Entity<User>()
            .HasMany(x => x.Events)
            .WithOne(x => x.Creator);

    }
}