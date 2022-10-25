using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;
using PickyBride.friend;
using PickyBride.hall;

namespace PickyBrideTests;

[TestFixture]
public class FriendTests
{
    private Friend _friend;
    private Hall _hall;

    [SetUp]
    public void SetUp()
    {
        _hall = new Hall(new ContenderGenerator());
        _friend = new Friend(_hall);
    }

    [Test]
    public void Friend_ShouldNotThrowsError_WhenBothContendersVisitedThePrincess()
    {
        var firstContender = _hall.GetNextContenderId();
        var secondContender = _hall.GetNextContenderId();
        _friend.Invoking(y => y.IsFirstBetterThenSecond(firstContender, secondContender))
            .Should()
            .NotThrow();
    }

    [Test]
    public void Friend_ShouldThrowsError_WhenOneOfContenderDidNotVisitedThePrincess()
    {
        var сontender = _hall.GetNextContenderId();
        _friend.Invoking(y => y.IsFirstBetterThenSecond(сontender, 500))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage("This contender is not visited the Princess");
    }

    [Test]
    public void Friend_ShouldCorrectlyComparingOfContenders()
    {
        var firstContenderId = _hall.GetNextContenderId();
        var secondContenderId = _hall.GetNextContenderId();
        var firstContender = _hall.GetVisitedContender(firstContenderId);
        var secondContender = _hall.GetVisitedContender(secondContenderId);
        var result = _friend.IsFirstBetterThenSecond(firstContenderId, secondContenderId);
        result.Should().Be(firstContender.Prettiness.CompareTo(secondContender.Prettiness));
    }
}