using ConsoleApp1.contender;

namespace ConsoleApp1.hall;

public class Hall : IHallForFriend, IHallForPrincess
{
    private readonly List<Contender> _waitingContenders;
    private readonly Dictionary<int, Contender> _visitedContenders;
    private readonly Random _random;

    public Hall(int maxNumberOfContenders)
    {
        var generator = new ContenderGenerator();
        _waitingContenders = generator.GetContenders(maxNumberOfContenders);
        _visitedContenders = new Dictionary<int, Contender>();
        _random = new Random(DateTime.Now.Millisecond);
    }

    public int GetNextContender()
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
            Console.WriteLine("#" + princeId + " : " + contender.Name + " " + contender.Patronymic + " : " +
                              contender.Prettiness);
        }
    }

    public int ChooseAPrince(int princeId)
    {
        //PrintAllVisitedContenders();
        if (princeId == -1)
        {
            Console.WriteLine("Result : " + 10);
            return 10;
        }
        if (_visitedContenders.Count >= 50)
        {
            Console.WriteLine("Result : " + 0);
            return 0;
        }
        Console.WriteLine("Chosen contender id = " + princeId + " : " + _visitedContenders[princeId].Prettiness);
        return _visitedContenders[princeId].Prettiness;
    }

    public int IsFirstBetterThenSecond(int first, int second)
    {
        if (!_visitedContenders.ContainsKey(first) || !_visitedContenders.ContainsKey(second)) return -1;
        var firstContender = _visitedContenders[first];
        var secondContender = _visitedContenders[second];
        return firstContender.IsBetter(secondContender) ? 1 : 0;
    }
}