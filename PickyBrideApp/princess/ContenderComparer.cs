

using HallWebApi.model.friend;

namespace PickyBride.princess;

public class ContenderComparer: IComparer<string> 
{
    private readonly IFriend _friend;
    
    public ContenderComparer(IFriend friend)
    {
        _friend = friend;
    }
    
    public int Compare(string? x, string? y)
    {
        if (x == null && y != null) return -1;
        if (x != null && y == null) return 1;
        if (x == null && y == null) return 0;
        var res = _friend.WhoIsBetter(x!, y!).Result!;
        if (res == "same names") return 0;
        return res == x ? 1 : -1;
    }
}