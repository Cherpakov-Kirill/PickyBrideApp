namespace PickyBride.contender;

public interface IContenderGenerator
{
    /// <summary>
    /// Returns generated list of contenders.
    /// </summary>
    /// <param name="numberOfContenders"></param>
    /// <returns>
    /// Contender list
    /// </returns>
    public List<Contender> GetContenders(int numberOfContenders);
}