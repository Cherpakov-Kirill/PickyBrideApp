using PickyBride.database;

namespace PickyBride.contender;

public class DbContenderLoader : IContenderGenerator
{
    private readonly IDbController _dbController;
    
    public DbContenderLoader(IDbController dbController)
    {
        _dbController = dbController;
    }

    public async Task<List<Contender>> GetContenders(int attemptNumber)
    {
        var attemptSteps = await _dbController.GetAllByAttemptNumber(attemptNumber);
        attemptSteps.Sort((firstAttemptStep, secondAttemptStep) =>
            firstAttemptStep.ContenderPosition.CompareTo(secondAttemptStep.ContenderPosition));
        return attemptSteps.ConvertAll(attemptStep => attemptStep.Contender.ToContender());
    }
}