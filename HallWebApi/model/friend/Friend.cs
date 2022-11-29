using HallWebApi.model.hall;

namespace HallWebApi.model.friend;

public class Friend : IFriend
{
    private readonly IHall _hallForFriend;

    public Friend(IHall hallForFriend)
    {
        _hallForFriend = hallForFriend;
    }

    public int IsFirstBetterThenSecond(string firstContenderFullName, string secondContenderFullName)
    {
        var firstContender = _hallForFriend.GetVisitedContender(firstContenderFullName);
        var secondContender = _hallForFriend.GetVisitedContender(secondContenderFullName);
        return firstContender.CompareTo(secondContender);
    }
}