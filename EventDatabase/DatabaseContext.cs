using EventDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventDatabase;

public partial class DatabaseContext : DbContext
{
    public DbSet<EventEntity> Events { get; set; }
    
    public string DbPath { get; set; }


    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = @"C:\Users\kR9_h\Desktop\МЗИ\EventApi\app.db";
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}