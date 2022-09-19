using ConsoleApp1.friend;
using ConsoleApp1.hall;

namespace ConsoleApp1.princess;

public class Princess
{
    private IHallForPrincess Hall;
    private IFriend Friend;
    private List<int> contenders;

    public Princess(IHallForPrincess hall, IFriend friend)
    {
        Hall = hall;
        Friend = friend;
        contenders = new List<int>();
    }

    private int FindPosition(int princeId)
    {
        if (contenders.Count == 0) return 0;
        var low = 0;
        var high = contenders.Count - 1;
        var resOfCompare = 0;
        while (low < high)
        {
            var mid = (low + high) / 2;
            var midPrinceId = contenders[mid];
            resOfCompare = Friend.IsFirstBetterThenSecond(princeId, midPrinceId);
            if (resOfCompare == 1)
            {
                low = mid + 1;
            }

            if (resOfCompare == 0)
            {
                high = mid - 1;
            }
        }

        resOfCompare = Friend.IsFirstBetterThenSecond(princeId, contenders[low]);
        return resOfCompare switch
        {
            1 => low + 1,
            0 => low,
            _ => -1
        };
    }

    private int AddNewPrince(int princeId)
    {
        var idx = FindPosition(princeId);
        contenders.Insert(idx, princeId);
        return idx;
    }

    public int FindPrince(int skip, int percent)
    {
        for (var i = 0; i < skip; i++)
        {
            var princeId = Hall.GetNextContender();
            AddNewPrince(princeId);
        }

        var idx = 0;
        var res = 0;
        while (idx < contenders.Count * percent / 100)
        {
            var princeId = Hall.GetNextContender();
            idx = AddNewPrince(princeId);
            res = contenders[idx];
            if (contenders.Count == 49) break;
        }

        return Hall.ChooseAPrince(res);
    }
}