using FluentAssertions;
using NUnit.Framework;
using PickyBride;
using PickyBride.contender;
using PickyBride.hall;

namespace PickyBrideTests;

[TestFixture]
public class HallTests
{
    private Hall _hall;
    private const int NumberOfContenders = 2;

    [SetUp]
    public void SetUp()
    {
        _hall = new Hall(new ContenderGenerator(), NumberOfContenders);
    }
    
    [Test]
    public void ShouldThrowsErrorWhenNoNewContendersInTheHall()
    {
        for (var i = 0; i < NumberOfContenders; i++)
        {
            _hall.LetTheNextContenderGoToThePrincess();
        }

        _hall.Invoking(y => y.LetTheNextContenderGoToThePrincess())
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(PickyBride.resources.NoNewContender);
    }
    
    [Test]
    public void ShouldReturnsNextContender()
    {
        var contenderId = _hall.LetTheNextContenderGoToThePrincess();
        const int expectedContenderId = 1;
        contenderId.Should().Be(expectedContenderId);
    }
    
    [Test]
    public void ShouldReturnsVisitedContenderData()
    {
        var contenderId = _hall.LetTheNextContenderGoToThePrincess();
        _hall.Invoking(y => y.GetVisitedContender(contenderId)).Should().NotThrow<ApplicationException>();
    }
    
    [Test]
    public void ShouldThrowsErrorWhenContenderDidNotVisitPrincess()
    {
        const int contenderId = 1;
        _hall.Invoking(y => y.GetVisitedContender(contenderId))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(PickyBride.resources.ThisContenderDidNotVisit);
    }
    
    [Test]
    public void ShouldReturnsCorrectContenderPrettiness()
    {
        var contenderId = _hall.LetTheNextContenderGoToThePrincess();
        var contender = _hall.GetVisitedContender(contenderId);
        contender.Prettiness.Should().Be(_hall.GetContenderPrettiness(contenderId));
    }
}