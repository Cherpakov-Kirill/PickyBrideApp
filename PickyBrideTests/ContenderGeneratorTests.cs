using FluentAssertions;
using HallWebApi.model.contender;
using HallWebApi.model.database;
using NUnit.Framework;

namespace PickyBrideTests;

[TestFixture]
public class ContenderGeneratorTests
{
    private const int NumberOfContenders = 100;

    [TestCase(1)]
    [TestCase(50)]
    [TestCase(100)]
    public async Task ShouldReturnsListWithNeedingSize(int numberOfContenders)
    {
        var dbController = new DbController(new InMemoryDbContext());
        var generator = new ContenderGenerator(dbController, numberOfContenders);
        var contenders = await generator.GetContenders(1);
        contenders.Count.Should().Be(numberOfContenders);
    }

    [TestCase(-1)]
    [TestCase(0)]
    public void ShouldThrowsErrorWhenNumberOfContendersLessThenOne(int numberOfContenders)
    {
        var dbController = new DbController(new InMemoryDbContext());
        var generator = new ContenderGenerator(dbController, numberOfContenders);
        generator.Invoking(async y => await y.GetContenders(numberOfContenders))
            .Should()
            .ThrowAsync<ApplicationException>()
            .WithMessage(HallWebApi.resources.NumberOfContendersShouldBeMoreThenZero);
    }

    [Test]
    public async Task ShouldReturnsUniqueNames()
    {
        var dbController = new DbController(new InMemoryDbContext());
        var generator = new ContenderGenerator(dbController, NumberOfContenders);
        var contenders = await generator.GetContenders(NumberOfContenders);
        contenders.Should().OnlyHaveUniqueItems(x => $"{x.Name} {x.Patronymic}");
    }
}