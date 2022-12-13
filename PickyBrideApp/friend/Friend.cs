using HallWebApi.model.dto;
using HallWebApi.model.friend;
using PickyBride.api;

namespace PickyBride.friend;

public class Friend : IFriend
{
    private int _attemptNumber;
    private readonly HttpController _httpController;
    
    public Friend(HttpController httpController)
    {
        _httpController = httpController;
    }
    
    public async Task<string> WhoIsBetter(string firstContenderFullName, string secondContenderFullName)
    {
        var content = new CompareContendersDto(firstContenderFullName, secondContenderFullName);
        return (await _httpController.SendPostRequest<ContenderNameDto, CompareContendersDto>($"/friend/{_attemptNumber}/compare", content))!.Name;
    }

    public void SetAttemptNumber(int newNumberOfAttempt)
    {
        _attemptNumber = newNumberOfAttempt;
    }
}