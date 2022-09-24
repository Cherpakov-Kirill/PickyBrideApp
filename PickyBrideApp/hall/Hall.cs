using PickyBride.contender;

namespace PickyBride.hall;

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

    /// <summary>
    /// Generates list of contenders.
    /// </summary>
    /// <param name="maxNumberOfContenders">number of contenders for generation</param>
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

    public void TakeAContenderAndPrintPrincessHappiness(int contenderId)
    {
        PrintAllVisitedContenders();
        if (contenderId == -1)
        {
            Console.WriteLine("Princess could not choose any contender. Princess happiness : " + NotTakenResult);
        }

        var takenContender = _visitedContenders[contenderId];

        if (takenContender.Prettiness <= DefeatThreshold)
        {
            Console.WriteLine("Princess choose contender with prettiness = {0}  Princess happiness : {1}",
                takenContender.Prettiness, DefeatResult);
        }

        Console.WriteLine("Taken contender : {0} {1} | Princess happiness : {2}", takenContender.Name,
            takenContender.Patronymic, takenContender.Prettiness);
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