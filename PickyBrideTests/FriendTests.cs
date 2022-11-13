using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;
using PickyBride.database;
using PickyBride.database.context;
using PickyBride.friend;
using PickyBride.hall;

namespace PickyBrideTests;

[TestFixture]
public class FriendTests
{
    private const int NumberOfContenders = 2;
    private Friend _friend;
    private Hall _hall;

    [SetUp]
    public void SetUp()
    {
        var dbController = new DbController(new InMemoryDbContext());
        var generator = new ContenderGenerator(dbController, NumberOfContenders);
        _hall = new Hall(generator);
        _friend = new Friend(_hall);
    }

    [Test]
    public void ShouldNotThrowsErrorWhenBothContendersVisitedThePrincess()
    {
        var firstContender = _hall.LetTheNextContenderGoToThePrincess();
        var secondContender = _hall.LetTheNextContenderGoToThePrincess();
        _friend.Invoking(y => y.IsFirstBetterThenSecond(firstContender, secondContender))
            .Should()
            .NotThrow();
    }

    [Test]
    public void ShouldThrowsErrorWhenFirstContenderDidNotVisitedThePrincess()
    {
        var contender = _hall.LetTheNextContenderGoToThePrincess();
        var notVisitedContender = contender + 1;
        _friend.Invoking(y => y.IsFirstBetterThenSecond(notVisitedContender, contender))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(PickyBride.resources.ThisContenderDidNotVisit);
    }
    
    [Test]
    public void ShouldThrowsErrorWhenSecondContenderDidNotVisitedThePrincess()
    {
        var contender = _hall.LetTheNextContenderGoToThePrincess();
        var notVisitedContender = contender + 1;
        _friend.Invoking(y => y.IsFirstBetterThenSecond(contender, notVisitedContender))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(PickyBride.resources.ThisContenderDidNotVisit);
    }
    
    [Test]
    public void ShouldThrowsErrorWhenBothContendersDidNotVisitedThePrincess()
    {
        const int firstContendersWithoutVisit = 1;
        const int secondContendersWithoutVisit = firstContendersWithoutVisit + 1;
        _friend.Invoking(y => y.IsFirstBetterThenSecond(secondContendersWithoutVisit, firstContendersWithoutVisit))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(PickyBride.resources.ThisContenderDidNotVisit);
    }

    [Test]
    public void ShouldCorrectlyComparesOfContenders()
    {
        var firstContenderId = _hall.LetTheNextContenderGoToThePrincess();
        var secondContenderId = _hall.LetTheNextContenderGoToThePrincess();
        var firstContender = _hall.GetVisitedContender(firstContenderId);
        var secondContender = _hall.GetVisitedContender(secondContenderId);
        var result = _friend.IsFirstBetterThenSecond(firstContenderId, secondContenderId);
        result.Should().Be(firstContender.Prettiness.CompareTo(secondContender.Prettiness));
    }
}