using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;
using PickyBride.friend;
using PickyBride.hall;
using PickyBride.princess;

namespace PickyBrideTests;

[TestFixture]
public class PrincessWithMockedHallTests
{
    private IContenderGenerator _generator;
    private Friend _friend;
    private Hall _hall;
    private Princess _princess;

    [SetUp]
    public void SetUpMockedClassStack()
    {
        var mockedHall = new MockedHall();
        _friend = new Friend(mockedHall);
        _princess = new Princess(mockedHall, _friend);
    }

    [Test]
    public void ShouldThrowsErrorWhenNoNewContendersInTheHall()
    {
        _princess.FindContender().Should().Be(Princess.NotTakenResult);
    }
}