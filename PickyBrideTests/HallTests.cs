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

    [SetUp]
    public void SetUp()
    {
        _hall = new Hall(new ContenderGenerator());
    }
    
    [Test]
    public void Hall_ShouldThrowsError_WhenNoNewContendersInTheHall()
    {
        for (var i = 0; i < Program.MaxNumberOfContenders; i++)
        {
            _hall.GetNextContenderId();
        }

        _hall.Invoking(y => y.GetNextContenderId())
            .Should()
            .Throw<ApplicationException>()
            .WithMessage("There are no new contenders for the princess in the hall");
    }
    
    [Test]
    public void Hall_ShouldReturnsNextContender()
    {
        var contenderId = _hall.GetNextContenderId();
        contenderId.Should().Be(1);
    }
}