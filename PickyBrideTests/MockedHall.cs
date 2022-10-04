using PickyBride;
using PickyBride.contender;
using PickyBride.hall;

namespace PickyBrideTests;

public class MockedHall : IHall
{
    private const int DefeatThreshold = 50;
    private const int DefeatResult = 0;
    private const int NotTakenResult = 10;
    
    private readonly List<Contender> _waitingContenders;
    private readonly Dictionary<int, Contender> _visitedContenders;

    public MockedHall(IContenderGenerator contenderGenerator)
    {
        _waitingContenders = contenderGenerator.GetContenders(Program.MaxNumberOfContenders);
        _waitingContenders.Sort((x, y) => y.CompareTo(x));
        _visitedContenders = new Dictionary<int, Contender>();
    }

    public int GetNextContenderId()
    {
        if (_waitingContenders.Count == 0)
        {
            throw new ApplicationException("There are no new contenders for the princess in the mocked hall");
        }

        var contender = _waitingContenders[0];

        _waitingContenders.Remove(contender);
        var currentNumber = _visitedContenders.Count + 1;
        _visitedContenders.Add(currentNumber, contender);

        return currentNumber;
    }

    public int ComputePrincessHappiness(int contenderId)
    {
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
}