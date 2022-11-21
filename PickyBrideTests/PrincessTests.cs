﻿using FluentAssertions;
using NUnit.Framework;
using PickyBride;
using PickyBride.contender;
using PickyBride.database;
using PickyBride.database.context;
using PickyBride.friend;
using PickyBride.hall;
using PickyBride.princess;

namespace PickyBrideTests;

[TestFixture]
public class PrincessTests
{
    private const int NumberOfAttempt = 1;
    private IContenderGenerator _generator;
    private Friend _friend;
    private Hall _hall;
    private Princess _princess;

    [SetUp]
    public void SetUpClassStack()
    {
        var dbController = new DbController(new InMemoryDbContext());
        _generator = new ContenderGenerator(dbController, Program.MaxNumberOfContenders);
        _hall = new Hall(_generator);
        _hall.Initialize(NumberOfAttempt);
        _friend = new Friend(_hall);
        _princess = new Princess(_hall, _friend);
    }

    [Test]
    public void ShouldCorrectlyChoosesContenderWithHighPrettiness()
    {
        //This test validates princess algorithm.
        //Algorithm relies on random order of contenders in queue.
        //Princess should get happiness greater than 95 at least.
        var happiness = _princess.FindContender();
        happiness.Should().BeGreaterThan(95);
    }
}