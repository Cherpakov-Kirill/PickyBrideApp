namespace PickyBride.friend;

public interface IFriend
{
    /// <summary>
    /// Compares two contenders on prettiness by them ids.
    /// </summary>
    /// <param name="firstContenderId">id of the first contender</param>
    /// <param name="secondContenderId">id of the second contender</param>
    /// <returns>
    /// Result of calling firstContenderPrettiness.CompareTo(secondContenderPrettiness)
    /// </returns>
    public int IsFirstBetterThenSecond(int firstContenderId, int secondContenderId);
}