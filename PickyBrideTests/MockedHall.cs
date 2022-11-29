using HallWebApi.model.contender;
using HallWebApi.model.hall;

namespace PickyBrideTests;

public class MockedHall : IHall
{
    private readonly List<int> _waitingContenders;
    private int _lastVisitedContenderIdx;

    private void GenerateContenderList()
    {
        for (var i = Convert.ToInt32(PickyBride.Program.MaxNumberOfContenders); i >= 1; i--) _waitingContenders.Add(i);
    }

    public MockedHall()
    {
        _lastVisitedContenderIdx = -1;
        _waitingContenders = new List<int>();
        GenerateContenderList();
    }

    public string? LetTheNextContenderGoToThePrincess()
    {
        if (_lastVisitedContenderIdx + 1  == PickyBride.Program.MaxNumberOfContenders)
            return null;
        _lastVisitedContenderIdx++;
        return $"{_lastVisitedContenderIdx}";
    }

    public int GetContenderPrettiness(string fullName)
    {
        var takenContender = GetVisitedContender(fullName);
        return takenContender.Prettiness;
    }

    public int SelectContender()
    {
        throw new NotImplementedException();
    }

    public Contender GetVisitedContender(string fullName)
    {
        return new Contender(
            $"MockedContenderName{fullName}",
            $"MockedContenderPatronymic{fullName}",
            _waitingContenders[Convert.ToInt32(fullName)]
        );
    }

    public Task Initialize(int newNumberOfAttempt)
    {
        throw new NotImplementedException();
    }
}