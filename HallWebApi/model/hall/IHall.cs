using HallWebApi.model.contender;

namespace HallWebApi.model.hall;

public interface IHall
{
    /// <summary>
    /// Returns unique id of next contender in queue for coming to princess.
    /// </summary>
    /// <returns>
    /// if hall has new contenders, than Contender full name = {name patronymic}. 
    /// if hall has not any new contender, than null. 
    /// </returns>
    public Task<string?> LetTheNextContenderGoToThePrincess();

    /// <summary>
    /// Method returns last visited contender prettiness
    /// </summary>
    /// <returns>last visited contender prettiness</returns>
    public Task<int> SelectContender();
    
    /// <summary>
    /// Returns contender object by contenderId.
    /// </summary>
    /// <param name="fullName"></param>
    /// <returns>
    /// Contender object.
    /// </returns>
    /// <exception cref="ApplicationException">throws when contender with this id had not visit the princess yet.</exception>
    public Contender GetVisitedContender(string fullName);

    /// <summary>
    /// Initialize all hall's data for starting new attempt
    /// </summary>
    /// <param name="newNumberOfAttempt">new number of attempt</param>
    Task Initialize(int newNumberOfAttempt);
}