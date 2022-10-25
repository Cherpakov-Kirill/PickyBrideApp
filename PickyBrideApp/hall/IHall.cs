using PickyBride.contender;

namespace PickyBride.hall;

public interface IHall
{
    /// <summary>
    /// Returns unique id of next contender in queue for coming to princess.
    /// </summary>
    /// <returns>
    /// Contender id of next contender.
    /// </returns>
    public int GetNextContenderId();

    /// <summary>
    /// Method returns chosen contender prettiness.
    /// </summary>
    /// <param name="contenderId"></param>
    /// <returns>
    /// Chosen contender prettiness.
    /// </returns>
    public int GetContenderPrettiness(int contenderId);
    
    /// <summary>
    /// Returns contender object by contenderId.
    /// </summary>
    /// <param name="contenderId">contender id</param>
    /// <returns>
    /// Contender object.
    /// </returns>
    /// <exception cref="ApplicationException">throws when contender with this id had not visit the princess yet.</exception>
    public Contender GetVisitedContender(int contenderId);
}