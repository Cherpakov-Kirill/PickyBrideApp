using PickyBride.hall;

namespace PickyBride.friend;

public class Friend : IFriend
{
    private readonly IHallForFriend _hallForFriend;

    public Friend(IHallForFriend hallForFriend)
    {
        _hallForFriend = hallForFriend;
    }

    public int IsFirstBetterThenSecond(int firstContenderId, int secondContenderId)
    {
        var firstContender = _hallForFriend.GetVisitedContender(firstContenderId);
        var secondContender = _hallForFriend.GetVisitedContender(secondContenderId);
        return firstContender.CompareTo(secondContender);
    }
}