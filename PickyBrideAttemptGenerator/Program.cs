using PickyBride.contender;
using PickyBride.database;
using PickyBride.database.context;

namespace PickyBrideAttemptGenerator;

public static class Program
{
    public static async Task Main()
    {
        var dbController = new DbController(new PostgresqlDbContext());
        var contenderGenerator = new ContenderGenerator(dbController, PickyBride.Program.MaxNumberOfContenders);
        
        for (var attemptNumber = 1; attemptNumber <= PickyBride.Program.NumberOfAttempts; attemptNumber++)
        {
            await contenderGenerator.GetContenders(attemptNumber);
        }
    }
}