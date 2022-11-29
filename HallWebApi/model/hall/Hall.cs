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
        Console.WriteLine("Hall Created!");
        _contenderGenerator = contenderGenerator;
        _visitedContenders = new Dictionary<string, Contender>();
        _waitingContenders = new List<Contender>();
        _lastContenderFullName = "";
    }

    public string? LetTheNextContenderGoToThePrincess()
    {
        if (_waitingContenders.Count == 0)
        {
            Console.WriteLine(resources.NoNewContender);
            return null;
        }

        var contender = _waitingContenders[0];

        _waitingContenders.Remove(contender);
        var fullName = MakeKeyForContender(contender.Name, contender.Patronymic);
        _visitedContenders.Add(fullName, contender);
        _lastContenderFullName = fullName;
        
        return fullName;
    }

    public int SelectContender()
    {
        return GetContenderPrettiness(_lastContenderFullName);
    }

    public int GetContenderPrettiness(string fullName)
    {
        var takenContender = GetVisitedContender(fullName);
        Console.Write(resources.ChosenContenderInfo, _attemptNumber, takenContender.Name,
            takenContender.Patronymic, takenContender.Prettiness);
        return takenContender.Prettiness;
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
        //Console.WriteLine("Hall Initialized!");
        _attemptNumber = newNumberOfAttempt;
        _waitingContenders = await _contenderGenerator.GetContenders(_attemptNumber);
        Console.WriteLine($"Hall Initialized! size = {_waitingContenders.Count}");
        _visitedContenders.Clear();
    }
}