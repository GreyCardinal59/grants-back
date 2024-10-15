using admin_back.Models;
using Microsoft.EntityFrameworkCore;

namespace admin_back.DataAccess;

public class UsersDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public UsersDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public DbSet<User> Users => Set<User>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }
}