﻿using FluentAssertions;
using HallWebApi.model.contender;
using HallWebApi.model.database;
using HallWebApi.model.hall;
using NUnit.Framework;

namespace PickyBrideTests;

[TestFixture]
public class HallTests
{
    private Hall _hall;
    private const int NumberOfContenders = 2;
    private const int NumberOfAttempt = 1;

    [SetUp]
    public async Task SetUp()
    {
        var dbController = new DbController(new InMemoryDbContext());
        var generator = new ContenderGenerator(dbController, NumberOfContenders);
        _hall = new Hall(generator);
        await _hall.Initialize(NumberOfAttempt);
    }
    
    [Test]
    public async Task ShouldReturnsNullWhenNoNewContendersInTheHall()
    {
        for (var i = 0; i < NumberOfContenders; i++)
        {
            await _hall.LetTheNextContenderGoToThePrincess();
        }

        (await _hall.LetTheNextContenderGoToThePrincess()).Should().BeNull();
    }
    
    [Test]
    public void ShouldReturnsNextContender()
    {
        var contenderId = _hall.LetTheNextContenderGoToThePrincess();
        contenderId.Should().NotBeNull();
    }
    
    [Test]
    public async Task ShouldReturnsVisitedContenderData()
    {
        var contenderName = await _hall.LetTheNextContenderGoToThePrincess();
        _hall.Invoking(y => y.GetVisitedContender(contenderName!)).Should().NotThrow<ApplicationException>();
    }
    
    [Test]
    public void ShouldThrowsErrorWhenContenderDidNotVisitPrincess()
    {
        const string contenderName = "Not visited Contender";
        _hall.Invoking(y => y.GetVisitedContender(contenderName))
            .Should()
            .Throw<ApplicationException>()
            .WithMessage(HallWebApi.resources.ThisContenderDidNotVisit);
    }
    
    [Test]
    public async Task ShouldReturnsCorrectContenderPrettiness()
    {
        var contenderName = await _hall.LetTheNextContenderGoToThePrincess();
        var contender = _hall.GetVisitedContender(contenderName!);
        contender.Prettiness.Should().Be(await _hall.SelectContender());
    }
}