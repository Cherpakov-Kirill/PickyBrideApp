using HallWebApi.model.dto;
using HallWebApi.model.friend;
using Microsoft.AspNetCore.Mvc;

namespace HallWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FriendController : Controller
{
    private readonly IFriend _friend;
    
    public FriendController(IFriend friend)
    {
        _friend = friend;
    }
    
    /// <summary>
    /// Compares two contenders by them names
    /// </summary>
    /// <param name="attemptNumber">Number of attempt</param>
    /// <param name="content">JSON with two contender names for comparing</param>
    /// <param name="session">Session number</param>
    /// <returns>Returns JSON with contender name</returns>
    [HttpPost("{attemptNumber:int}/compare")]
    [ProducesResponseType(typeof(ContenderNameDto), 200)]
    public IActionResult Compare(int attemptNumber, CompareContendersDto content, int session)
    {
        var result = _friend.WhoIsBetter(content.FirstName, content.SecondName);
        return Ok(new ContenderNameDto(result));
    }
}