using PickyBride.contender;

namespace PickyBride.hall;

public class Hall : IHall
{
    private readonly List<Contender> _waitingContenders;
    private readonly Dictionary<int, Contender> _visitedContenders;
    private readonly Random _random;

    public Hall(IContenderGenerator contenderGenerator)
    {
        _waitingContenders = contenderGenerator.GetContenders(Program.MaxNumberOfContenders);
        _visitedContenders = new Dictionary<int, Contender>();
        _random = new Random(DateTime.Now.Millisecond);
    }
    
    public int GetNextContenderId()
    {
        if (_waitingContenders.Count == 0)
        {
            throw new ApplicationException("There are no new contenders for the princess in the hall");
        }

        var randomValue = _random.Next(0, _waitingContenders.Count - 1);
        var contender = _waitingContenders[randomValue];

        _waitingContenders.Remove(contender);
        var currentNumber = _visitedContenders.Count + 1;
        _visitedContenders.Add(currentNumber, contender);

        return currentNumber;
    }

    public int GetContenderPrettiness(int contenderId)
    {
        PrintAllVisitedContenders();
        var takenContender = GetVisitedContender(contenderId);
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
}