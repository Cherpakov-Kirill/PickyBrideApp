using PickyBride.contender;

namespace PickyBride.hall;

public interface IHallForFriend
{
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