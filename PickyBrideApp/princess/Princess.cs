using PickyBride.friend;
using PickyBride.hall;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PickyBride.princess;

public class Princess : IHostedService
{
    private const int DefeatThreshold = 50;
    private const int DefeatResult = 0;
    private const int NotTakenResult = 10;
    
    private const int NumberOfSkippingContenders = 50;
    private readonly IHall _hall;
    private readonly IFriend _friend;
    private readonly List<int> _contenders;
    
    private readonly ILogger<Princess>? _logger;
    private readonly IHostApplicationLifetime? _appLifetime;

    public Princess(ILogger<Princess>? logger,
        IHostApplicationLifetime? appLifetime, IHall hall, IFriend friend)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _appLifetime?.ApplicationStarted.Register(OnStarted);
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
        _logger?.LogInformation("Princess : StartAsync has been called.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger?.LogInformation("Princess : StopAsync has been called.");
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        FindContender();
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
            var contenderId = _hall.GetNextContenderId();
            AddNewContender(contenderId);
        }

        var idx = 0;
        var res = 0;
        while (idx < _contenders.Count - 1)
        {
            var contenderId = _hall.GetNextContenderId();
            idx = AddNewContender(contenderId);
            res = _contenders[idx];
        }

        return ComputePrincessHappiness(res);
    }
    
    private int ComputePrincessHappiness(int contenderId)
    {
        if (contenderId == -1)
        {
            Console.WriteLine("Princess could not choose any contender. Princess happiness : " + NotTakenResult);
            return NotTakenResult;
        }

        var chosenContenderPrettiness = _hall.GetContenderPrettiness(contenderId);

        if (chosenContenderPrettiness > DefeatThreshold) return chosenContenderPrettiness;
        
        Console.WriteLine("Princess choose contender with prettiness = {0}  Princess happiness : {1}",
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