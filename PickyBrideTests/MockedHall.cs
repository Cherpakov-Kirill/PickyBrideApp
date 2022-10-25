using PickyBride;
using PickyBride.contender;
using PickyBride.hall;

namespace PickyBrideTests;

public class MockedHall : IHall
{
    private readonly List<int> _waitingContenders;
    private int _lastVisitedContenderIdx;

    private void GenerateContenderList()
    {
        for (var i = Program.MaxNumberOfContenders; i >= 1; i--) _waitingContenders.Add(i);
    }

    public MockedHall()
    {
        _lastVisitedContenderIdx = -1;
        _waitingContenders = new List<int>();
        GenerateContenderList();
    }

    public int GetNextContenderId()
    {
        _lastVisitedContenderIdx++;
        return _lastVisitedContenderIdx;
    }

    public int GetContenderPrettiness(int contenderId)
    {
        var takenContender = GetVisitedContender(contenderId);
        return takenContender.Prettiness;
    }

    public Contender GetVisitedContender(int contenderId)
    {
        return new Contender(
            $"MockedContenderName{contenderId}",
            $"MockedContenderPatronymic{contenderId}",
            _waitingContenders[contenderId]
        );
    }
}