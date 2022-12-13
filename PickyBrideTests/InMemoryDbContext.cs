using HallWebApi.model.database.context;
using Microsoft.EntityFrameworkCore;

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