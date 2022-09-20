using ConsoleApp1.contender;

namespace ConsoleApp1.hall;

public class Hall : IHallForFriend, IHallForPrincess
{
    private const int DefeatThreshold = 50;
    private const int DefeatResult = 0;
    private const int NotTakenResult = 10;


    private List<Contender> _waitingContenders;
    private readonly Dictionary<int, Contender> _visitedContenders;
    private readonly Random _random;
    private readonly ContenderGenerator _generator;

    public Hall()
    {
        _waitingContenders = new List<Contender>();
        _generator = new ContenderGenerator();
        _visitedContenders = new Dictionary<int, Contender>();
        _random = new Random(DateTime.Now.Millisecond);
    }

    public void GenerateContenders(int maxNumberOfContenders)
    {
        _waitingContenders = _generator.GetContenders(maxNumberOfContenders);
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

    private void PrintAllVisitedContenders()
    {
        for (var princeId = 1; princeId <= _visitedContenders.Count; princeId++)
        {
            var contender = _visitedContenders[princeId];
            Console.WriteLine("#{0} : {1} {2} : {3}",
                princeId, contender.Name, contender.Patronymic, contender.Prettiness);
        }
    }

    public int TakeAPrince(int princeId)
    {
        PrintAllVisitedContenders();
        if (princeId == -1)
        {
            Console.WriteLine("Result : " + NotTakenResult);
            return 10;
        }

        var takenContender = _visitedContenders[princeId];

        if (takenContender.Prettiness <= DefeatThreshold)
        {
            Console.WriteLine("Result : " + DefeatResult);
            return 0;
        }


        Console.WriteLine("Taken contender : {0} {1} | Prettiness : {2}", takenContender.Name,
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
}