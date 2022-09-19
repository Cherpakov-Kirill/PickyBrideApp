using ConsoleApp1.hall;

namespace ConsoleApp1.friend;

public class Friend : IFriend
{
    private IHallForFriend HallForFriend;

    public Friend(IHallForFriend hallForFriend)
    {
        HallForFriend = hallForFriend;
    }

    public int IsFirstBetterThenSecond(int first, int second)
    {
        return HallForFriend.IsFirstBetterThenSecond(first, second);
    }
}