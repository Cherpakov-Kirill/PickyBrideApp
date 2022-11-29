namespace HallWebApi.model.friend;

public interface IFriend
{
    /// <summary>
    /// Compares two contenders on prettiness by them ids.
    /// </summary>
    /// <param name="firstContenderFullName">full name of the first contender</param>
    /// <param name="secondContenderFullName">full name of the second contender</param>
    /// <returns>
    /// Result of calling firstContenderPrettiness.CompareTo(secondContenderPrettiness)
    /// </returns>
    public int IsFirstBetterThenSecond(string firstContenderFullName, string secondContenderFullName);
}