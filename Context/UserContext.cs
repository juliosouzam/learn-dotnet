using Microsoft.EntityFrameworkCore;
using zoeira.Models;

namespace zoeira.Context
{
  public class UserContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=localhost;Database=zoeira;Username=postgres;Password=admin");
  }
}