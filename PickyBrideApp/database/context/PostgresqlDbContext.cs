using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace PickyBride.database.context;

public class PostgresqlDbContext : BaseDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConfigurationManager.AppSettings["PostgreSQLConnectionString"]);
    }
    
    public override BaseDbContext GetDbContext()
    {
        return new PostgresqlDbContext();
    }
}