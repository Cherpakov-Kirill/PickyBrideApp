using Microsoft.EntityFrameworkCore;

namespace HallWebApi.model.database.context;

public class PostgresqlDbContext : BaseDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(System.Configuration.ConfigurationManager.AppSettings["PostgreSQLConnectionString"]);
    }
    
    public override BaseDbContext GetDbContext()
    {
        return new PostgresqlDbContext();
    }
}