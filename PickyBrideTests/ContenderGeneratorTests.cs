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
    public void ShouldReturnsListWithNeedingSize(int numberOfContenders)
    {
        var generator = new ContenderGenerator();
        var contenders = generator.GetContenders(numberOfContenders);
        contenders.Count.Should().Be(numberOfContenders);
    }

    [TestCase(-1)]
    [TestCase(0)]
    public void ShouldThrowsErrorWhenNumberOfContendersLessThenOne(int numberOfContenders)
    {
        var generator = new ContenderGenerator();
        generator.Invoking(y => y.GetContenders(numberOfContenders))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(PickyBride.resources.number_of_contenders_should_be_more_then_zero);
        ;
    }

    [Test]
    public void ShouldReturnsUniqueNames()
    {
        var generator = new ContenderGenerator();
        var contenders = generator.GetContenders(NumberOfContenders);
        contenders.Should().OnlyHaveUniqueItems(x => $"{x.Name} {x.Patronymic}");
    }
}