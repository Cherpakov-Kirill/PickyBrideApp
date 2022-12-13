using HallWebApi.model.contender;

namespace HallWebApi.model.hall;

public class Hall : IHall
{
    private static string MakeKeyForContender(string name, string patronymic)
    {
        return $"{name} {patronymic}";
    }
    
    private readonly IContenderGenerator _contenderGenerator;
    private List<Contender> _waitingContenders;
    private readonly Dictionary<string, Contender> _visitedContenders;
    private int _attemptNumber;
    private string _lastContenderFullName;

    public Hall(IContenderGenerator contenderGenerator)
    {
        _contenderGenerator = contenderGenerator;
        _visitedContenders = new Dictionary<string, Contender>();
        _waitingContenders = new List<Contender>();
        _lastContenderFullName = "";
    }

    public Task<string?> LetTheNextContenderGoToThePrincess()
    {
        string? fullName;
        if (_waitingContenders.Count == 0)
        {
            Console.WriteLine(resources.NoNewContender);
            fullName = null;
        }
        else
        {
            var contender = _waitingContenders[0];
            _waitingContenders.Remove(contender);
            fullName = MakeKeyForContender(contender.Name, contender.Patronymic);
            _visitedContenders.Add(fullName, contender);
            _lastContenderFullName = fullName;
        }
        return Task.FromResult(fullName);
    }

    public Task<int> SelectContender()
    {
        var takenContender = GetVisitedContender(_lastContenderFullName);
        Console.WriteLine(resources.ChosenContenderInfo, _attemptNumber, takenContender.Name,
            takenContender.Patronymic, takenContender.Prettiness);
        return Task.FromResult(takenContender.Prettiness);
    }

    public Contender GetVisitedContender(string fullName)
    {
        if (!_visitedContenders.ContainsKey(fullName))
        {
            throw new ApplicationException(resources.ThisContenderDidNotVisit);
        }

        return _visitedContenders[fullName];
    }

    public async Task Initialize(int newNumberOfAttempt)
    {
        _attemptNumber = newNumberOfAttempt;
        _waitingContenders = await _contenderGenerator.GetContenders(_attemptNumber);
        _visitedContenders.Clear();
    }
}