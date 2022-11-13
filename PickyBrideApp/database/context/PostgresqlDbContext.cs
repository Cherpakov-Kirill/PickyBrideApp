using Microsoft.EntityFrameworkCore;
namespace PickyBride.database.context;

public class PostgresqlDbContext : AbstractDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Server=localhost;Database=picky-bride;
                		User Id=picky-bride;Password=1234567890");
    }
    
    public override AbstractDbContext GetDbContext()
    {
        return new PostgresqlDbContext();
    }
}