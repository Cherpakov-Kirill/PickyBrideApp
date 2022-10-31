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
    
    public Hall(IContenderGenerator contenderGenerator, int numberOfContenders)
    {
        _waitingContenders = contenderGenerator.GetContenders(numberOfContenders);
        _visitedContenders = new Dictionary<int, Contender>();
        _random = new Random(DateTime.Now.Millisecond);
    }
    
    public int LetTheNextContenderGoToThePrincess()
    {
        if (_waitingContenders.Count == 0)
        {
            throw new ApplicationException(resources.NoNewContender);
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
        Console.WriteLine(resources.ChosenContenderInfo, takenContender.Name,
            takenContender.Patronymic, takenContender.Prettiness);
        return takenContender.Prettiness;
    }

    public Contender GetVisitedContender(int contenderId)
    {
        if (!_visitedContenders.ContainsKey(contenderId))
        {
            throw new ApplicationException(resources.ThisContenderDidNotVisit);
        }

        return _visitedContenders[contenderId];
    }

    private void PrintAllVisitedContenders()
    {
        for (var contenderId = 1; contenderId <= _visitedContenders.Count; contenderId++)
        {
            var contender = _visitedContenders[contenderId];
            Console.WriteLine(resources.ContenderInfo,
                contenderId, contender.Name, contender.Patronymic, contender.Prettiness);
        }
    }
}