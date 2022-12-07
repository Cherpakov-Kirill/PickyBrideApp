using FluentAssertions;
using HallWebApi.model.contender;
using HallWebApi.model.database;
using HallWebApi.model.friend;
using HallWebApi.model.hall;
using NUnit.Framework;

namespace PickyBrideTests;

[TestFixture]
public class FriendTests
{
    private const int NumberOfContenders = 2;
    private const int NumberOfAttempt = 1;
    private Friend _friend;
    private Hall _hall;

    [SetUp]
    public async Task SetUp()
    {
        var dbController = new DbController(new InMemoryDbContext());
        var generator = new ContenderGenerator(dbController, NumberOfContenders);
        _hall = new Hall(generator);
        await _hall.Initialize(NumberOfAttempt);
        _friend = new Friend(_hall);
    }

    [Test]
    public async Task ShouldNotThrowsErrorWhenBothContendersVisitedThePrincess()
    {
        var firstContender = await _hall.LetTheNextContenderGoToThePrincess();
        var secondContender = await _hall.LetTheNextContenderGoToThePrincess();
        _friend.Invoking(y => y.WhoIsBetter(firstContender!, secondContender!))
            .Should()
            .NotThrow();
    }

    [Test]
    public async Task ShouldThrowsErrorWhenFirstContenderDidNotVisitedThePrincess()
    {
        var contender = await _hall.LetTheNextContenderGoToThePrincess();
        const string notVisitedContender = "Not visited Contender";
        _friend.Invoking(y => y.WhoIsBetter(notVisitedContender, contender!))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(HallWebApi.resources.ThisContenderDidNotVisit);
    }
    
    [Test]
    public async Task ShouldThrowsErrorWhenSecondContenderDidNotVisitedThePrincess()
    {
        var contender = await _hall.LetTheNextContenderGoToThePrincess();
        const string notVisitedContender = "Not visited Contender";
        _friend.Invoking(y => y.WhoIsBetter(contender!, notVisitedContender))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(HallWebApi.resources.ThisContenderDidNotVisit);
    }
    
    [Test]
    public void ShouldThrowsErrorWhenBothContendersDidNotVisitedThePrincess()
    {
        const string firstContendersWithoutVisit = "First Not visited Contender";
        const string secondContendersWithoutVisit = "Second Not visited Contender";
        _friend.Invoking(y => y.WhoIsBetter(secondContendersWithoutVisit, firstContendersWithoutVisit))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(HallWebApi.resources.ThisContenderDidNotVisit);
    }

    [Test]
    public async Task ShouldCorrectlyComparesOfContenders()
    {
        var firstContenderName = await _hall.LetTheNextContenderGoToThePrincess();
        var secondContenderName = await _hall.LetTheNextContenderGoToThePrincess();
        var firstContender = _hall.GetVisitedContender(firstContenderName!);
        var secondContender = _hall.GetVisitedContender(secondContenderName!);
        var result = _friend.WhoIsBetter(firstContenderName!, secondContenderName!);
        result.Should().Be(firstContender.Prettiness > secondContender.Prettiness ? firstContenderName : secondContenderName);
    }
}