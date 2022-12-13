using HallWebApi.model.contender;
using HallWebApi.model.database.entity;

namespace HallWebApi.model.database;

public interface IDbController
{
    /// <summary>
    /// Returns all records with attemptNumber from DB 
    /// </summary>
    /// <param name="attemptNumber">number of searched attempt</param>
    /// <returns>
    /// Async task with Enumerator with records with attemptNumber from DB
    /// </returns>
    public Task<List<AttemptStepEntity>> GetAllByAttemptNumber(int attemptNumber);

    /// <summary>
    /// Adds list of new records about contender for current attempt to DB
    /// </summary>
    /// <param name="contenders">list of contender object</param>
    /// <param name="attemptNumber">attempt number for contender</param>
    /// <returns>
    /// Async task
    /// </returns>
    public Task SaveAllContendersToDb(List<Contender> contenders, int attemptNumber);
}