using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;
using PickyBride.database;

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
    public void ShouldLoadContenderListFromDbWithNeedingSize()
    {
        const int attemptNumber = 1;
        var dbController = new DbController(new InMemoryDbContext());
        var generator = new ContenderGenerator(dbController, NumberOfContenders);
        var loader = new DbContenderLoader(dbController);
        var contenders = generator.GetContenders(attemptNumber);
        var loadedContenders = loader.GetContenders(attemptNumber);
        loadedContenders.Should().BeEquivalentTo(contenders);
    }
}