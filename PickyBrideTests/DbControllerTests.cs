using FluentAssertions;
using NUnit.Framework;
using PickyBride.contender;
using PickyBride.database;

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
    [TestCase(1)]
    [TestCase(50)]
    [TestCase(100)]
    public async Task ShouldAddAndGetListWithNeedingSize(int numberOfContenders)
    {
        var dbController = new DbController(new InMemoryDbContext());
        var contender = new Contender("Kirill", "Maksimovich", 100);
        const int attemptNumber = 1;
        var contenders = Enumerable.Repeat(contender, numberOfContenders).ToList();
        await dbController.SaveAllContendersToDb(contenders,attemptNumber);
        
        var records = dbController.GetAllByAttemptNumber(attemptNumber).Result;
        records.Count.Should().Be(numberOfContenders);
        for(var num = 0; num < numberOfContenders; num++)
        {
            var position = num + 1;
            var record = records[num];
            
            record.Id.Should().Be(position);
            record.AttemptNumber.Should().Be(attemptNumber);
            record.ContenderPosition.Should().Be(position);
            
            record.Contender.Name.Should().Be(contender.Name);
            record.Contender.Patronymic.Should().Be(contender.Patronymic);
            record.Contender.Prettiness.Should().Be(contender.Prettiness);
        }
    }

    [Test]
    public async Task ShouldGetRecordListWithRightAttemptNumber()
    {
        var dbController = new DbController(new InMemoryDbContext());
        var firstContender = new Contender("Kirill", "Maksimovich", 100);
        var secondContender = new Contender("Artem", "Maksimovich", 100);
        const int firstAttemptNumber = 1;
        const int secondAttemptNumber = 2;
        const int thirdAttemptNumber = 3;
        const int contenderPosition = 1;

        await dbController.SaveAllContendersToDb(new List<Contender> { firstContender }, firstAttemptNumber);
        await dbController.SaveAllContendersToDb(new List<Contender> { secondContender }, secondAttemptNumber);
        
        var firstAttemptRecords = dbController.GetAllByAttemptNumber(firstAttemptNumber).Result;
        firstAttemptRecords.Count.Should().Be(1);
        firstAttemptRecords[0].Contender.Name.Should().Be(firstContender.Name);
        firstAttemptRecords[0].Contender.Patronymic.Should().Be(firstContender.Patronymic);
        firstAttemptRecords[0].Contender.Prettiness.Should().Be(firstContender.Prettiness);
        
        var secondAttemptRecords = dbController.GetAllByAttemptNumber(secondAttemptNumber).Result;
        secondAttemptRecords.Count.Should().Be(1);
        secondAttemptRecords[0].Contender.Name.Should().Be(secondContender.Name);
        secondAttemptRecords[0].Contender.Patronymic.Should().Be(secondContender.Patronymic);
        secondAttemptRecords[0].Contender.Prettiness.Should().Be(secondContender.Prettiness);
        
        var thirdAttemptRecords = dbController.GetAllByAttemptNumber(thirdAttemptNumber).Result;
        thirdAttemptRecords.Count.Should().Be(0);
    }
}