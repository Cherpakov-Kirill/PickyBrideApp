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

    public Task<string?> LetTheNextContenderGoToThePrincess()
    {
        string? fullName;
        if (_lastVisitedContenderIdx + 1 == PickyBride.Program.MaxNumberOfContenders)
        {
            fullName = null;
        }
        else
        {
            _lastVisitedContenderIdx++;
            fullName = $"{_lastVisitedContenderIdx}";
        }
        return Task.FromResult(fullName);
    }

    public Task<int> SelectContender()
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