using FluentAssertions;
using HallWebApi.model.contender;
using HallWebApi.model.database;
using NUnit.Framework;

namespace PickyBrideTests;

[TestFixture]
public class DbContenderLoaderTests
{
    private const int NumberOfContenders = 100;
    
    [SetUp]
    public void ClearDatabase()
    {
        using var context = new InMemoryDbContext();
        context.Database.EnsureDeleted();
    }
    
    [Test]
    public async Task ShouldLoadContenderListFromDbWithNeedingSize()
    {
        const int attemptNumber = 1;
        var dbController = new DbController(new InMemoryDbContext());
        var generator = new ContenderGenerator(dbController, NumberOfContenders);
        var loader = new DbContenderLoader(dbController);
        var contenders = await generator.GetContenders(attemptNumber);
        var loadedContenders = await loader.GetContenders(attemptNumber);
        loadedContenders.Should().BeEquivalentTo(contenders);
    }
}