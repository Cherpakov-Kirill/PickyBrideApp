using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PickyBride.hall;

namespace PickyBride.friend;

public class Friend : IFriend, IHostedService
{
    private readonly IHall _hallForFriend;
    
    private readonly ILogger<Friend> _logger;

    public Friend(ILogger<Friend> logger, IHall hallForFriend)
    {
        _logger = logger;
        _hallForFriend = hallForFriend;
    }

    public int IsFirstBetterThenSecond(int firstContenderId, int secondContenderId)
    {
        var firstContender = _hallForFriend.GetVisitedContender(firstContenderId);
        var secondContender = _hallForFriend.GetVisitedContender(secondContenderId);
        return firstContender.CompareTo(secondContender);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Friend : StartAsync has been called.");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Friend : StopAsync has been called.");

        return Task.CompletedTask;
    }
}