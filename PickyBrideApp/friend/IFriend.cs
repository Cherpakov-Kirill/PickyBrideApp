namespace PickyBride.friend;

public interface IFriend
{
    /// <summary>
    /// Compares two contenders on prettiness by them ids.
    /// </summary>
    /// <param name="firstContenderId">id of the first contender</param>
    /// <param name="secondContenderId">id of the second contender</param>
    /// <returns>
    /// <list type="table">
    /// <item>
    /// <term><b>Returns</b></term>
    /// <description>
    /// <b>Description</b>
    /// </description>
    /// </item>
    /// <item>
    /// <term>Less than zero</term>
    /// <description>
    /// First contender is worse than second contender.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description>
    /// First contender is equal to second contender.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <description>
    /// First contender is better than second contender.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    public int IsFirstBetterThenSecond(int firstContenderId, int secondContenderId);
}