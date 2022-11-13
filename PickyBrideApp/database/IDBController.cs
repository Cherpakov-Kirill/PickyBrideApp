using PickyBride.contender;
using PickyBride.database.entity;

namespace PickyBride.database;

public interface IDbController
{
    /// <summary>
    /// Returns all records with attemptNumber from DB 
    /// </summary>
    /// <param name="attemptNumber">number of searched attempt</param>
    /// <returns>
    /// Enumerator with records with attemptNumber from DB
    /// </returns>
    public List<AttemptStepEntity> GetAllByAttemptNumber(int attemptNumber);

    /// <summary>
    /// Adds new record about contender for current attempt to DB : 
    /// </summary>
    /// <param name="contender">contender object</param>
    /// <param name="attemptNumber">attempt number for contender</param>
    /// <param name="contenderPosition">order number of contender</param>
    public void Add(Contender contender, int attemptNumber, int contenderPosition);
}