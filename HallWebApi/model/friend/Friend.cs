using HallWebApi.model.hall;

namespace HallWebApi.model.friend;

public class Friend : IFriend
{
    private readonly IHall _hallForFriend;

    public Friend(IHall hallForFriend)
    {
        _hallForFriend = hallForFriend;
    }

    public Task<string> WhoIsBetter(string firstContenderFullName, string secondContenderFullName)
    {
        var firstContender = _hallForFriend.GetVisitedContender(firstContenderFullName);
        var secondContender = _hallForFriend.GetVisitedContender(secondContenderFullName);
        var result = firstContender.CompareTo(secondContender) == 1 ? firstContenderFullName : secondContenderFullName;
        return Task.FromResult(result);
    }

    public void SetAttemptNumber(int newNumberOfAttempt)
    {
        throw new NotImplementedException();
    }
}