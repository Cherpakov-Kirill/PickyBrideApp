using ConsoleApp1.hall;

namespace ConsoleApp1.friend;

public class Friend : IFriend
{
    private readonly IHallForFriend _hallForFriend;

    public Friend(IHallForFriend hallForFriend)
    {
        _hallForFriend = hallForFriend;
    }

    public int IsFirstBetterThenSecond(int first, int second)
    {
        return _hallForFriend.IsFirstBetterThenSecond(first, second);
    }
}