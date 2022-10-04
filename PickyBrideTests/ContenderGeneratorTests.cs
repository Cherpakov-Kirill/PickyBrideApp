using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;

namespace PickyBrideTests;

[TestFixture]
public class ContenderGeneratorTests
{
    private const int NumberOfContenders = 100;

    [TestCase(1)]
    [TestCase(50)]
    [TestCase(100)]
    public void ContenderGenerator_ShouldReturnsListWithNeedingSize(int numberOfContenders)
    {
        var generator = new ContenderGenerator();
        var contenders = generator.GetContenders(numberOfContenders);
        contenders.Count.Should().Be(numberOfContenders);
    }

    [TestCase(-1)]
    [TestCase(-50)]
    [TestCase(-100)]
    public void ContenderGenerator_ShouldThrowsError_WhenNumberOfContendersLessThenZero(int numberOfContenders)
    {
        var generator = new ContenderGenerator();
        generator.Invoking(y => y.GetContenders(numberOfContenders))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage("numberOfContenders should be more then zero");
        ;
    }

    [Test]
    public void ContenderGenerator_ShouldReturnsUniqueNames()
    {
        var generator = new ContenderGenerator();
        var contenders = generator.GetContenders(NumberOfContenders);
        contenders.Should().OnlyHaveUniqueItems(x => x.Name + " " + x.Patronymic);
    }
}