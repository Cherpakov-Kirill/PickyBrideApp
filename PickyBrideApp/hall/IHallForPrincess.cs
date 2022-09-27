namespace PickyBride.hall;

public interface IHallForPrincess
{
    /// <summary>
    /// Returns unique id of next contender in queue for coming to princess.
    /// </summary>
    /// <returns>
    /// Contender id of next contender.
    /// </returns>
    public int GetNextContenderId();

    /// <summary>
    /// This method choose a contender by unique contender id, prints happiness level of princess.
    /// </summary>
    /// <param name="contenderId">
    /// unique contender id that got from GetNextContenderId().
    /// (contenderId == -1) means that princess did not choose a contender.
    /// </param>
    /// <returns>
    /// Princess happiness after choosing a contender.
    /// </returns>
    public int ComputePrincessHappiness(int contenderId);
}