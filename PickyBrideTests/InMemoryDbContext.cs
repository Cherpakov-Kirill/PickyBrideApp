using Microsoft.EntityFrameworkCore;
using PickyBride.database.context;

namespace PickyBrideTests;

public class InMemoryDbContext : BaseDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "picky-bride");
    }

    public override BaseDbContext GetDbContext()
    {
        return new InMemoryDbContext();
    }
}