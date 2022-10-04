using PickyBride.hall;

namespace PickyBride.friend;

public class Friend : IFriend
{
    private readonly IHall _hallForFriend;

    public Friend(IHall hallForFriend)
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