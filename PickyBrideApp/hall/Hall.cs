using PickyBride.contender;

namespace PickyBride.hall;

public class Hall : IHall
{
    private readonly IContenderGenerator _contenderGenerator;
    private List<Contender> _waitingContenders;
    private readonly Dictionary<int, Contender> _visitedContenders;
    private int _attemptNumber;

    public Hall(IContenderGenerator contenderGenerator)
    {
        _contenderGenerator = contenderGenerator;
        _visitedContenders = new Dictionary<int, Contender>();
        _waitingContenders = new List<Contender>();
    }

    public int LetTheNextContenderGoToThePrincess()
    {
        if (_waitingContenders.Count == 0)
        {
            throw new ApplicationException(resources.NoNewContender);
        }

        var contender = _waitingContenders[0];

        _waitingContenders.Remove(contender);
        var contenderPosition = _visitedContenders.Count + 1;
        _visitedContenders.Add(contenderPosition, contender);
        
        return contenderPosition;
    }

    public int GetContenderPrettiness(int contenderId)
    {
        var takenContender = GetVisitedContender(contenderId);
        Console.WriteLine(resources.ChosenContenderInfo, _attemptNumber, contenderId, takenContender.Name,
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

    public void Initialize(int newNumberOfAttempt)
    {
        _attemptNumber = newNumberOfAttempt;
        _waitingContenders = _contenderGenerator.GetContenders(_attemptNumber);
        _visitedContenders.Clear();
    }
}