using PickyBride.friend;
using PickyBride.hall;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PickyBride.princess;

public class Princess : IHostedService
{
    public const int NotTakenResult = 10;
    private const int DefeatThreshold = 50;
    private const int DefeatResult = 0;
    private const int PrincessDidNotTakeAnyOne = -1;

    private const int NumberOfSkippingContenders = 37;
    private const int NumberOfTheBestContendersInTheEndOfSortedList = 4;
    private readonly int _numberOfRuns;
    private readonly int _emulationAttemptNumber;
    private readonly IHall _hall;
    private readonly IFriend _friend;
    private readonly List<int> _contenders;
    private readonly IHostApplicationLifetime? _appLifetime;

    public Princess(IHostApplicationLifetime? appLifetime, IHall hall, IFriend friend, int numberOfRuns = 1,
        int emulationAttemptNumber = 1)
    {
        _appLifetime = appLifetime;
        _appLifetime?.ApplicationStarted.Register(OnStarted);
        _hall = hall;
        _friend = friend;
        _contenders = new List<int>();
        _numberOfRuns = numberOfRuns;
        _emulationAttemptNumber = emulationAttemptNumber;
    }

    public Princess(IHall hall, IFriend friend)
    {
        _hall = hall;
        _friend = friend;
        _contenders = new List<int>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        if (_numberOfRuns == 1)
        {
            _hall.Initialize(_emulationAttemptNumber);
            FindContender();
        }
        else
        {
            var sum = 0.0;
            for (var currentAttemptNumber = 1; currentAttemptNumber <= _numberOfRuns; currentAttemptNumber++)
            {
                _contenders.Clear();
                _hall.Initialize(currentAttemptNumber);
                sum += FindContender();
            }

            var avg = Math.Round(sum / (float)_numberOfRuns, 2);
            Console.WriteLine(resources.AvgOfPrincessHappiness, avg);
        }

        _appLifetime?.StopApplication();
    }

    /// <summary>
    /// Find a contender for marriage.
    /// </summary>
    /// <returns>Level of princess happiness after choose of prince.</returns>
    public int FindContender()
    {
        for (var i = 0; i < NumberOfSkippingContenders; i++)
        {
            var contenderId = _hall.LetTheNextContenderGoToThePrincess();
            AddNewContender(contenderId);
        }

        var idx = 0;
        var res = 0;
        while (idx < _contenders.Count - NumberOfTheBestContendersInTheEndOfSortedList)
        {
            if (_contenders.Count == Program.MaxNumberOfContenders)
                return ComputePrincessHappiness(PrincessDidNotTakeAnyOne);
            var contenderId = _hall.LetTheNextContenderGoToThePrincess();
            idx = AddNewContender(contenderId);
            res = _contenders[idx];
        }

        return ComputePrincessHappiness(res);
    }

    private int ComputePrincessHappiness(int contenderId)
    {
        if (contenderId == PrincessDidNotTakeAnyOne)
        {
            Console.WriteLine(resources.PrincessCouldNotChooseAnyContenderResult, NotTakenResult);
            return NotTakenResult;
        }

        var chosenContenderPrettiness = _hall.GetContenderPrettiness(contenderId);

        if (chosenContenderPrettiness > DefeatThreshold) return chosenContenderPrettiness;

        Console.WriteLine(resources.PrincessChoseThePrinceResult_,
            chosenContenderPrettiness, DefeatResult);
        return DefeatResult;
    }

    private int AddNewContender(int contenderId)
    {
        _contenders.Add(contenderId);
        _contenders.Sort((firstContenderId, secondContenderId) =>
            _friend.IsFirstBetterThenSecond(firstContenderId, secondContenderId));
        return _contenders.IndexOf(contenderId);
    }
}