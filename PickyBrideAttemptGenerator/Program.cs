using HallWebApi.model.database;
using HallWebApi.model.database.context;
using HallWebApi.model.contender;

namespace PickyBrideAttemptGenerator;

public static class Program
{
    private const int MaxNumberOfContenders = 100;
    private const int NumberOfAttempts = 100;
    public static async Task Main()
    {
        var dbController = new DbController(new PostgresqlDbContext());
        var contenderGenerator = new ContenderGenerator(dbController, MaxNumberOfContenders);
        
        for (var attemptNumber = 1; attemptNumber <= NumberOfAttempts; attemptNumber++)
        {
            await contenderGenerator.GetContenders(attemptNumber);
        }
    }
}