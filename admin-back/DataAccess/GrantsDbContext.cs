using admin_back.Models;
using Microsoft.EntityFrameworkCore;

namespace admin_back.DataAccess;

public class GrantsDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public GrantsDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Grant> Grants => Set<Grant>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }
}