using PickyBride;
using PickyBride.contender;
using PickyBride.hall;

namespace PickyBrideTests;

public class MockedHall : IHall
{
    private readonly List<int> _waitingContenders;
    private readonly Dictionary<int, int> _visitedContenders;

    private void GenerateContenderList()
    {
        for (var i = Program.MaxNumberOfContenders; i >= 1; i--) _waitingContenders.Add(i);
    }

    public MockedHall()
    {
        _waitingContenders = new List<int>();
        GenerateContenderList();
        _visitedContenders = new Dictionary<int, int>();
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

    public int GetContenderPrettiness(int contenderId)
    {
        var takenContender = GetVisitedContender(contenderId);
        return takenContender.Prettiness;
    }

    public Contender GetVisitedContender(int contenderId)
    {
        if (!_visitedContenders.ContainsKey(contenderId))
        {
            throw new ApplicationException("This contender is not visited the Princess");
        }

        return new Contender($"MockedContenderName{contenderId}", $"MockedContenderPatronymic{contenderId}", _visitedContenders[contenderId]);
    }
}