using PickyBride.contender;

namespace PickyBride.hall;

public class Hall : IHall, IHostedService
{
    private const int DefeatThreshold = 50;
    private const int DefeatResult = 0;
    private const int NotTakenResult = 10;
    
    private readonly List<Contender> _waitingContenders;
    private readonly Dictionary<int, Contender> _visitedContenders;
    private readonly Random _random;
    
    private readonly ILogger<Hall> _logger;

    public Hall(ILogger<Hall> logger, IContenderGenerator contenderGenerator)
    {
        _logger = logger;
        _waitingContenders = contenderGenerator.GetContenders(Program.MaxNumberOfContenders);
        _visitedContenders = new Dictionary<int, Contender>();
        _random = new Random(DateTime.Now.Millisecond);
    }
    
    public int GetNextContenderId()
    {
        if (_waitingContenders.Count == 0) return -1;

        var randomValue = _random.Next(0, _waitingContenders.Count - 1);
        var contender = _waitingContenders[randomValue];

        _waitingContenders.Remove(contender);
        var currentNumber = _visitedContenders.Count + 1;
        _visitedContenders.Add(currentNumber, contender);

        return currentNumber;
    }

    public int ComputePrincessHappiness(int contenderId)
    {
        PrintAllVisitedContenders();
        if (contenderId == -1)
        {
            Console.WriteLine("Princess could not choose any contender. Princess happiness : " + NotTakenResult);
            return NotTakenResult;
        }

        var takenContender = GetVisitedContender(contenderId);

        if (takenContender.Prettiness <= DefeatThreshold)
        {
            Console.WriteLine("Princess choose contender with prettiness = {0}  Princess happiness : {1}",
                takenContender.Prettiness, DefeatResult);
            return DefeatResult;
        }

        Console.WriteLine("Taken contender : {0} {1} | Princess happiness : {2}", takenContender.Name,
            takenContender.Patronymic, takenContender.Prettiness);
        return takenContender.Prettiness;
    }

    public Contender GetVisitedContender(int contenderId)
    {
        if (!_visitedContenders.ContainsKey(contenderId))
        {
            throw new ApplicationException("This contender is not visited the Princess");
        }

        return _visitedContenders[contenderId];
    }

    private void PrintAllVisitedContenders()
    {
        for (var contenderId = 1; contenderId <= _visitedContenders.Count; contenderId++)
        {
            var contender = _visitedContenders[contenderId];
            Console.WriteLine("#{0} : {1} {2} : {3}",
                contenderId, contender.Name, contender.Patronymic, contender.Prettiness);
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hall : StartAsync has been called.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hall : StopAsync has been called.");
        return Task.CompletedTask;
    }
}