namespace PickyBride.contender;

public interface IContenderGenerator
{
    /// <summary>
    /// Returns list of contenders.
    /// </summary>
    /// <param name="attemptNumber">number of attempt</param>
    /// <returns>
    /// Contender list
    /// </returns>
    public List<Contender> GetContenders(int attemptNumber);
}