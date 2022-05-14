using Microsoft.EntityFrameworkCore;
using PizzaApp.Entities;

namespace PizzaApp.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User>Users { get; set; }
}