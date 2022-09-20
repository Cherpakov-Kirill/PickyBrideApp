using ConsoleApp1.friend;
using ConsoleApp1.hall;

namespace ConsoleApp1.princess;

public class Princess
{
    private readonly IHallForPrincess _hall;
    private readonly IFriend _friend;
    private readonly List<int> _contenders;

    public Princess(IHallForPrincess hall, IFriend friend)
    {
        _hall = hall;
        _friend = friend;
        _contenders = new List<int>();
    }

    private int AddNewPrince(int princeId)
    {
        _contenders.Add(princeId);
        _contenders.Sort((i1, i2) => _friend.IsFirstBetterThenSecond(i1, i2));
        return _contenders.IndexOf(princeId);
    }

    public void FindPrince(int skip)
    {
        for (var i = 0; i < skip; i++)
        {
            var princeId = _hall.GetNextContenderId();
            AddNewPrince(princeId);
        }

        var idx = 0;
        var res = 0;
        while (idx < _contenders.Count - 1)
        {
            var princeId = _hall.GetNextContenderId();
            idx = AddNewPrince(princeId);
            res = _contenders[idx];
        }

        _hall.TakeAPrince(res);
    }
}