﻿using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;
using PickyBride.friend;
using PickyBride.hall;
using PickyBride.princess;

namespace PickyBrideTests;

[TestFixture]
public class PrincessWithMockedHallTests
{
    private Princess _princess;

    [SetUp]
    public void SetUpMockedClassStack()
    {
        var mockedHall = new MockedHall();
        var friend = new Friend(mockedHall);
        _princess = new Princess(mockedHall, friend);
    }
    
    /// <summary>
    /// Mocked hall gives contenders ordered in descending order of the prettiness.
    /// According to the princess algorithm: she takes first best contender after NumberOfSkippingContenders.
    /// But no one contender can be better then previous contenders in this MockedHall.
    /// </summary>

    [Test]
    public void ShouldThrowsErrorWhenContendersVisitsThePrincessInDescendingOrderOfThePrettiness()
    {
        _princess.FindContender().Should().Be(Princess.NotTakenResult);
    }
}