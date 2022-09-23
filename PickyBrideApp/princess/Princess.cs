using PickyBride.friend;
using PickyBride.hall;

namespace PickyBride.princess;

public class Princess
{
    private const int NumberOfSkippingContenders = 50;
    private readonly IHallForPrincess _hall;
    private readonly IFriend _friend;
    private readonly List<int> _contenders;

    public Princess(IHallForPrincess hall, IFriend friend)
    {
        _hall = hall;
        _friend = friend;
        _contenders = new List<int>();
    }

    /// <summary>
    /// Find a contender for marriage.
    /// </summary>
    public void FindContender()
    {
        for (var i = 0; i < NumberOfSkippingContenders; i++)
        {
            var contenderId = _hall.GetNextContenderId();
            AddNewContender(contenderId);
        }

        var idx = 0;
        var res = 0;
        while (idx < _contenders.Count - 1)
        {
            var contenderId = _hall.GetNextContenderId();
            idx = AddNewContender(contenderId);
            res = _contenders[idx];
        }

        _hall.TakeAContender(res);
    }

    private int AddNewContender(int contenderId)
    {
        _contenders.Add(contenderId);
        _contenders.Sort((firstContenderId, secondContenderId) =>
            _friend.IsFirstBetterThenSecond(firstContenderId, secondContenderId));
        return _contenders.IndexOf(contenderId);
    }
}