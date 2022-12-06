namespace HallWebApi.model.friend;

public interface IFriend
{
    /// <summary>
    /// Compares two contenders on prettiness by them ids.
    /// </summary>
    /// <param name="firstContenderFullName">full name of the first contender</param>
    /// <param name="secondContenderFullName">full name of the second contender</param>
    /// <returns>
    /// Name of the contender , that better then other contender.
    /// </returns>
    public string WhoIsBetter(string firstContenderFullName, string secondContenderFullName);
    
    /// <summary>
    /// Sets current attempt number to friend
    /// </summary>
    /// <param name="newNumberOfAttempt">new number of attempt</param>
    public void SetAttemptNumber(int newNumberOfAttempt);
}