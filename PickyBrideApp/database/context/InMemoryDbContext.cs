using Microsoft.EntityFrameworkCore;
namespace PickyBride.database.context;

public class InMemoryDbContext : AbstractDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "picky-bride");
    }

    public override AbstractDbContext GetDbContext()
    {
        return new InMemoryDbContext();
    }
}