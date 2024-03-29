﻿using FluentAssertions;
using HallWebApi.model.contender;
using HallWebApi.model.database;
using HallWebApi.model.friend;
using HallWebApi.model.hall;
using NUnit.Framework;
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
    public async Task SetUpClassStack()
    {
        var dbController = new DbController(new InMemoryDbContext());
        _generator = new ContenderGenerator(dbController, PickyBride.Program.MaxNumberOfContenders);
        _hall = new Hall(_generator);
        await _hall.Initialize(NumberOfAttempt);
        _friend = new Friend(_hall);
        _princess = new Princess(_hall, _friend);
    }

    [Test]
    public async Task ShouldCorrectlyChoosesContenderWithHighPrettiness()
    {
        //This test validates princess algorithm.
        //Algorithm relies on random order of contenders in queue.
        //Princess should get happiness one of {0,100,50,20}.
        var happiness = await _princess.FindContender();
        happiness.Should().BeOneOf(
            Princess.DefeatResult,
            Princess.TwentyResult,
            Princess.FiftyResult,
            Princess.HundredResult
        );
    }
}