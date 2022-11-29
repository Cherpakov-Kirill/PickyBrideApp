using HallWebApi.model.friend;
using HallWebApi.model.hall;
using Microsoft.AspNetCore.Mvc;

namespace HallWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HallController : ControllerBase
{
    private readonly IHall _hall;
    private readonly IFriend _friend;
    private static int _currentNumberOfAttempt;

    public HallController(IHall hall, IFriend friend)
    {
        _hall = hall;
        _friend = friend;
    }

    private async Task InitializeNewAttempt(int newAttemptNumber)
    {
        _currentNumberOfAttempt = newAttemptNumber;
        await _hall.Initialize(_currentNumberOfAttempt);
    }

    [HttpPost("{attemptNumber:int}/next")]
    public async Task<IActionResult> Next(int attemptNumber, int session)
    {
        if (_currentNumberOfAttempt != attemptNumber) await InitializeNewAttempt(attemptNumber);
        
        var fullName = _hall.LetTheNextContenderGoToThePrincess();
        return Ok(new { name = fullName });
    }
    
    [HttpPost("{attemptNumber:int}/select")]
    public IActionResult Select(int attemptNumber, int session)
    {
        var contenderRank = _hall.SelectContender();
        return Ok(new { rank = contenderRank });
    }
}