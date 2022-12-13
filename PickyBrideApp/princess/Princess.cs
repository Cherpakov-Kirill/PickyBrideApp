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

    private const int NumberOfSkippingContenders = 45;
    private const int NumberOfTheBestContendersInTheEndOfSortedList = 4;
    private readonly IHall _hall;
    private readonly IFriend _friend;
    private readonly List<string> _contenders;
    private readonly IHostApplicationLifetime? _appLifetime;
    
    private int _currentAttemptNumber;
    private float _princessHappinessSum;

    public Princess(IHostApplicationLifetime? appLifetime, IHall hall, IFriend friend)
    {
        _appLifetime = appLifetime;
        _hall = hall;
        _friend = friend;
        _contenders = new List<string>();
        _currentAttemptNumber = 0;
        _princessHappinessSum = 0;
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
        _currentAttemptNumber = 1;
        await _hall.Initialize(_currentAttemptNumber);
        _friend.SetAttemptNumber(_currentAttemptNumber);
        await InitiateGettingOfContender();
    }

    /// <summary>
    /// Reinitialize services for next princess attempt of finding a prince.
    /// </summary>
    /// <param name="princessHappiness">princess happiness on current attempt</param>
    private async Task RepeatPrincessLaunch(int princessHappiness)
    {
        _princessHappinessSum += princessHappiness;
        if (_currentAttemptNumber < Program.NumberOfAttempts)
        {
            _currentAttemptNumber++;
            _contenders.Clear();
            await _hall.Initialize(_currentAttemptNumber);
            _friend.SetAttemptNumber(_currentAttemptNumber);
            await InitiateGettingOfContender();
        }
        else
        {
            var avg = Math.Round(_princessHappinessSum / (float) Program.NumberOfAttempts, 2);
            Console.WriteLine(HallWebApi.resources.AvgOfPrincessHappiness, avg);

            _appLifetime?.StopApplication();
        }
    }

    /// <summary>
    /// Initiates of getting a contender for marriage.
    /// </summary>
    /// <returns>Async Task</returns>
    private Task InitiateGettingOfContender()
    {
        return _hall.LetTheNextContenderGoToThePrincess();
    }

    /// <summary>
    /// Consumes a new contender from Hall. Check on princess criteria. Continue finding.
    /// </summary>
    /// <param name="contenderName">New contender full name</param>
    public async Task ConsumeContender(string? contenderName)
    {
        if (contenderName == null)
        {
            await RepeatPrincessLaunch(await ComputePrincessHappiness(null));
            return;
        }
        
        if (_contenders.Count < NumberOfSkippingContenders)
        {
            AddNewContender(contenderName);
            await InitiateGettingOfContender();
        }
        else
        {
            var newContenderPosition = AddNewContender(contenderName);
            if (newContenderPosition < _contenders.Count - NumberOfTheBestContendersInTheEndOfSortedList)
            {
                await InitiateGettingOfContender();
            }
            else
            {
                await RepeatPrincessLaunch(await ComputePrincessHappiness(contenderName));
            }
        }
    }

    private async Task<int> ComputePrincessHappiness(string? contenderName)
    {
        if (contenderName == null)
        {
            Console.WriteLine(HallWebApi.resources.PrincessCouldNotChooseAnyContenderResult, NotTakenResult);
            return NotTakenResult;
        }

        var chosenContenderPrettiness = await _hall.SelectContender();

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

    private async Task<int> Comparison(string firstContenderFullName, string secondContenderFullName)
    {
        return await _friend.WhoIsBetter(firstContenderFullName, secondContenderFullName) == firstContenderFullName
            ? 1
            : -1;
    }

    private int AddNewContender(string contenderName)
    {
        _contenders.Add(contenderName);
        _contenders.Sort((firstContenderFullName, secondContenderFullName) => Comparison(firstContenderFullName, secondContenderFullName).Result);
        return _contenders.IndexOf(contenderName);
    }
}