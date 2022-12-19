using System.Text;
using PickyBride.friend;
using PickyBride.hall;
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

    private int NumberOfSkippingContenders;
    private int NumberOfTheBestContendersInTheEndOfSortedList;
    private readonly IHall _hall;
    private readonly IFriend _friend;
    private readonly List<int> _contenders;
    private readonly IHostApplicationLifetime? _appLifetime;
    private int _currentAttemptNumber;

    public Princess(IHostApplicationLifetime? appLifetime, IHall hall, IFriend friend)
    {
        _appLifetime = appLifetime;
        _hall = hall;
        _friend = friend;
        _contenders = new List<int>();
    }

    public Princess(IHall hall, IFriend friend)
    {
        _hall = hall;
        _friend = friend;
        _contenders = new List<int>();
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
        var csvFile = new StringBuilder();

        csvFile.Append("#,1");
        for (NumberOfTheBestContendersInTheEndOfSortedList = 2;
             NumberOfTheBestContendersInTheEndOfSortedList <= 20;
             NumberOfTheBestContendersInTheEndOfSortedList++)
        {
            csvFile.Append($",{NumberOfTheBestContendersInTheEndOfSortedList}");
        }
        csvFile.Append('\n');

        for (NumberOfSkippingContenders = 71; NumberOfSkippingContenders <= 95; NumberOfSkippingContenders++)
        {
            var csvLine = new StringBuilder();
            csvLine.Append($"{NumberOfSkippingContenders}");
            
            for (NumberOfTheBestContendersInTheEndOfSortedList = 1;
                 NumberOfTheBestContendersInTheEndOfSortedList <= 20;
                 NumberOfTheBestContendersInTheEndOfSortedList++)
            {
                var sum = 0.0;
                for (_currentAttemptNumber = 1; _currentAttemptNumber <= Program.NumberOfAttempts; _currentAttemptNumber++)
                {
                    _contenders.Clear();
                    await _hall.Initialize(_currentAttemptNumber);
                    sum += FindContender();
                }

                var avg = Math.Round(sum / (float)Program.NumberOfAttempts, 2);
                csvLine.Append($",\"{avg}\"");
                Console.WriteLine($"skip={NumberOfSkippingContenders}, bestOffset={NumberOfTheBestContendersInTheEndOfSortedList}, avg={avg}");
            }

            csvFile.AppendLine(csvLine.ToString());
        }
        File.WriteAllText(@"C:\Users\cherp\OneDrive\Desktop\out.csv", csvFile.ToString());

        _appLifetime?.StopApplication();
    }

    /// <summary>
    /// Find a contender for marriage.
    /// </summary>
    /// <returns>Level of princess happiness after choose of prince.</returns>
    public int FindContender()
    {
        int contenderId = 0;
        for (var i = 0; i < NumberOfSkippingContenders; i++)
        {
            contenderId = _hall.LetTheNextContenderGoToThePrincess();
            AddNewContender(contenderId);
        }

        var idx = 0;
        var res = contenderId;
        while (idx < _contenders.Count - NumberOfTheBestContendersInTheEndOfSortedList)
        {
            if (_contenders.Count == Program.MaxNumberOfContenders)
                return ComputePrincessHappiness(PrincessDidNotTakeAnyOne);
            contenderId = _hall.LetTheNextContenderGoToThePrincess();
            idx = AddNewContender(contenderId);
            res = _contenders[idx];
        }
        //Console.Write($"NumberOFVisitedConteders = {_contenders.Count} | listIDx = {idx} | ");
        return ComputePrincessHappiness(res);
    }

    private int ComputePrincessHappiness(int contenderId)
    {
        if (contenderId == PrincessDidNotTakeAnyOne)
        {
            //Console.WriteLine(resources.PrincessCouldNotChooseAnyContenderResult, NotTakenResult);
            return NotTakenResult;
        }

        var chosenContenderPrettiness = _hall.GetContenderPrettiness(contenderId);

        var princessHappiness = chosenContenderPrettiness switch
        {
            100 => TwentyResult, // if contender prettiness = 100, then princess happiness = 20
            98 => FiftyResult, // if contender prettiness = 98, then princess happiness = 50 
            96 => HundredResult, // if contender prettiness = 96, then princess happiness = 100
            _ => DefeatResult // otherwise princess happiness = 0
        };
        //Console.WriteLine(resources.PrincessHappinessIs, princessHappiness);
        return princessHappiness;
    }

    private int AddNewContender(int contenderId)
    {
        _contenders.Add(contenderId);
        _contenders.Sort((firstContenderId, secondContenderId) =>
            _friend.IsFirstBetterThenSecond(firstContenderId, secondContenderId));
        return _contenders.IndexOf(contenderId);
    }
}