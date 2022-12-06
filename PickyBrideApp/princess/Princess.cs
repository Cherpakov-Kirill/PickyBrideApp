using HallWebApi.model.friend;
using HallWebApi.model.hall;
using Microsoft.Extensions.Hosting;

namespace PickyBride.princess;

public class Princess : IHostedService
{
    public const int NotTakenResult = 10;
    public const int TwentyResult = 20;
    public const int FiftyResult = 50;
    public const int HundredResult = 100;
    public const int DefeatResult = 0;
    private const int PrincessDidNotTakeAnyOne = -1;

    private const int NumberOfSkippingContenders = 45;
    private const int NumberOfTheBestContendersInTheEndOfSortedList = 4;
    private readonly IHall _hall;
    private readonly IFriend _friend;
    private readonly List<string> _contenders;
    private readonly IHostApplicationLifetime? _appLifetime;

    public Princess(IHostApplicationLifetime? appLifetime, IHall hall, IFriend friend)
    {
        _appLifetime = appLifetime;
        _hall = hall;
        _friend = friend;
        _contenders = new List<string>();
    }

    public Princess(IHall hall, IFriend friend)
    {
        _hall = hall;
        _friend = friend;
        _contenders = new List<string>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return OnStarted();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task OnStarted()
    {
        var sum = 0.0;
        for (var currentAttemptNumber = 1; currentAttemptNumber <= Program.NumberOfAttempts; currentAttemptNumber++)
        {
            _contenders.Clear();
            await _hall.Initialize(currentAttemptNumber);
            _friend.SetAttemptNumber(currentAttemptNumber);
            sum += FindContender();
        }

        var avg = Math.Round(sum / (float) Program.NumberOfAttempts, 2);
        Console.WriteLine(HallWebApi.resources.AvgOfPrincessHappiness, avg);

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
            var contenderName = _hall.LetTheNextContenderGoToThePrincess();
            if (contenderName != null) AddNewContender(contenderName);
        }

        var idx = 0;
        var res = "";
        while (idx < _contenders.Count - NumberOfTheBestContendersInTheEndOfSortedList)
        {
            var contenderName = _hall.LetTheNextContenderGoToThePrincess();
            if (contenderName == null) return ComputePrincessHappiness(null);

            idx = AddNewContender(contenderName);
            res = contenderName;
        }

        return ComputePrincessHappiness(res);
    }

    private int ComputePrincessHappiness(string? contenderName)
    {
        if (contenderName == null)
        {
            Console.WriteLine(HallWebApi.resources.PrincessCouldNotChooseAnyContenderResult, NotTakenResult);
            return NotTakenResult;
        }

        var chosenContenderPrettiness = _hall.SelectContender();

        var princessHappiness = chosenContenderPrettiness switch
        {
            100 => TwentyResult, // if contender prettiness = 100, then princess happiness = 20
            98 => FiftyResult, // if contender prettiness = 98, then princess happiness = 50 
            96 => HundredResult, // if contender prettiness = 96, then princess happiness = 100
            _ => DefeatResult // otherwise princess happiness = 0
        };
        Console.WriteLine(HallWebApi.resources.PrincessHappinessIs, princessHappiness);
        return princessHappiness;
    }

    private int AddNewContender(string contenderName)
    {
        _contenders.Add(contenderName);
        _contenders.Sort((firstContenderFullName, secondContenderFullName) =>
            _friend.WhoIsBetter(firstContenderFullName, secondContenderFullName)==firstContenderFullName ? 1 : -1);
        return _contenders.IndexOf(contenderName);
    }
}