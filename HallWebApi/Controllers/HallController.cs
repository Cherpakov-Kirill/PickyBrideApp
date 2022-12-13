using HallWebApi.model.dto;
using HallWebApi.model.hall;
using Microsoft.AspNetCore.Mvc;

namespace HallWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HallController : ControllerBase
{
    private readonly IHall _hall;
    private static int _currentNumberOfAttempt;

    public HallController(IHall hall)
    {
        _hall = hall;
    }

    private Task InitializeNewAttempt(int newAttemptNumber)
    {
        _currentNumberOfAttempt = newAttemptNumber;
        return _hall.Initialize(_currentNumberOfAttempt);
    }
    
    /// <summary>
    /// Returns next contender name from Hall
    /// </summary>
    /// <param name="attemptNumber">Number of attempt</param>
    /// <param name="session">Session number</param>
    /// <returns>JSON with next contender name</returns>
    [HttpPost("{attemptNumber:int}/next")]
    [ProducesResponseType(typeof(ContenderNameDto), 200)]
    public async Task<IActionResult> Next(int attemptNumber, int session)
    {
        if (_currentNumberOfAttempt != attemptNumber) await InitializeNewAttempt(attemptNumber);
        
        await _hall.LetTheNextContenderGoToThePrincess();
        return Ok();
    }
    
    /// <summary>
    /// Returns selected contender rank
    /// </summary>
    /// <param name="attemptNumber">Number of attempt</param>
    /// <param name="session">Session number</param>
    /// <returns>JSON with contender rank</returns>
    [HttpPost("{attemptNumber:int}/select")]
    [ProducesResponseType(typeof(ContenderRankDto), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> Select(int attemptNumber, int session)
    {
        if (_currentNumberOfAttempt != attemptNumber) return BadRequest("Current  Hall attempt not equal with attemptNumber in request");
        
        var contenderRank = await _hall.SelectContender();
        return Ok(new ContenderRankDto(contenderRank));
    }
}