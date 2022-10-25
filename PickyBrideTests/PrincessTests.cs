using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;
using PickyBride.friend;
using PickyBride.hall;
using PickyBride.princess;

namespace PickyBrideTests;

[TestFixture]
public class PrincessTests
{
    private IContenderGenerator _generator;
    private Friend _friend;
    private Hall _hall;
    private Princess _princess;

    private void SetUpClassStack()
    {
        _generator = new ContenderGenerator();
        _hall = new Hall(_generator);
        _friend = new Friend(_hall);
        _princess = new Princess(_hall, _friend);
    }

    private void SetUpMockedClassStack()
    {
        var mockedHall = new MockedHall();
        _friend = new Friend(mockedHall);
        _princess = new Princess(mockedHall, _friend);
    }

    [Test]
    public void Princess_ShouldCorrectlyChooseContenderWithHighPrettiness()
    {
        SetUpClassStack();
        var happiness = _princess.FindContender();
        happiness.Should().BeGreaterThan(95);
    }

    [Test]
    public void Princess_ShouldThrowsError_WhenNoNewContendersInTheHall()
    {
        SetUpMockedClassStack();
        _princess.Invoking(y => y.FindContender())
            .Should()
            .Throw<ApplicationException>()
            .WithMessage("There are no new contenders for the princess in the mocked hall");
    }
}