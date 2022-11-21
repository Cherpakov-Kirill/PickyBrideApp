using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;
using PickyBride.database;
using PickyBride.database.entity;

namespace PickyBrideTests;

[TestFixture]
public class DbControllerTests
{
    [SetUp]
    public void ClearDatabase()
    {
        using var context = new InMemoryDbContext();
        context.Database.EnsureDeleted();
    }

    [TestCase(0)]
    [TestCase(100)]
    public async Task ShouldAddAndGetListWithNeedingSize(int numberOfContenders)
    {
        var dbController = new DbController(new InMemoryDbContext());
        var contender = new Contender("Kirill", "Maksimovich", 100);
        const int attemptNumber = 1;
        var contenders = Enumerable.Repeat(contender, numberOfContenders).ToList();
        await dbController.SaveAllContendersToDb(contenders,attemptNumber);
        
        var position = 1;
        var expectedRecords = contenders.Select(contenderFromList => new AttemptStepEntity()
        {
            Id = position,
            Contender = new ContenderEntity()
                { Id = position, Name = contenderFromList.Name, Patronymic = contenderFromList.Patronymic, Prettiness = contenderFromList.Prettiness },
            AttemptNumber = attemptNumber,
            ContenderPosition = position++
        });
        
        var records = dbController.GetAllByAttemptNumber(attemptNumber).Result;
        records.Should().Equal(expectedRecords);
    }

    [Test]
    public Task ShouldGetEmptyRecordListWhenNoData()
    {
        const int attemptNumber = 1;
        var dbController = new DbController(new InMemoryDbContext());
        var attemptRecords = dbController.GetAllByAttemptNumber(attemptNumber).Result;
        attemptRecords.Count.Should().Be(0);
        return Task.CompletedTask;
    }

    
    /// <summary>
    /// Test checks right get attempt records by attempt number
    /// </summary>
    [Test]
    public async Task ShouldGetRecordListWithRightAttemptNumber()
    {
        var dbController = new DbController(new InMemoryDbContext());
        var firstContender = new Contender("Kirill", "Maksimovich", 100);
        var firstContenders = new List<Contender> { firstContender };
        var secondContender = new Contender("Artem", "Maksimovich", 100);
        var secondContenders = new List<Contender> { secondContender };
        const int firstAttemptNumber = 1;
        const int secondAttemptNumber = 2;

        await dbController.SaveAllContendersToDb(firstContenders, firstAttemptNumber);
        await dbController.SaveAllContendersToDb(secondContenders, secondAttemptNumber);

        var id = 1;
        var position = 1;
        var firstAttemptExpectedRecords = firstContenders.Select(contenderFromList => new AttemptStepEntity()
        {
            Id = id,
            Contender = new ContenderEntity()
                { Id = id++, Name = contenderFromList.Name, Patronymic = contenderFromList.Patronymic, Prettiness = contenderFromList.Prettiness },
            AttemptNumber = firstAttemptNumber,
            ContenderPosition = position++
        });
        var firstAttemptRecords = dbController.GetAllByAttemptNumber(firstAttemptNumber).Result;
        firstAttemptRecords.Should().Equal(firstAttemptExpectedRecords);
        
        position = 1;
        var secondAttemptExpectedRecords = secondContenders.Select(contenderFromList => new AttemptStepEntity()
        {
            Id = id,
            Contender = new ContenderEntity()
                { Id = id++, Name = contenderFromList.Name, Patronymic = contenderFromList.Patronymic, Prettiness = contenderFromList.Prettiness },
            AttemptNumber = secondAttemptNumber,
            ContenderPosition = position++
        });
        var secondAttemptRecords = dbController.GetAllByAttemptNumber(secondAttemptNumber).Result;
        secondAttemptRecords.Should().Equal(secondAttemptExpectedRecords);
    }
}