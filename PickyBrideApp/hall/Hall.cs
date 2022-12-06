using HallWebApi.model.contender;
using HallWebApi.model.hall;
using HallWebApi.model.dto;
using PickyBride.api;

namespace PickyBride.hall;

public class Hall : IHall
{
    private int _attemptNumber;
    private readonly HttpController _httpController;

    public Hall(HttpController httpController)
    {
        _httpController = httpController;
    }

    public string LetTheNextContenderGoToThePrincess()
    {
        var response = _httpController.SendPostRequest<ContenderNameDto>($"/hall/{_attemptNumber}/next").Result;
        return response!.Name;
    }

    public int SelectContender()
    {
        return _httpController.SendPostRequest<ContenderRankDto>($"/hall/{_attemptNumber}/select").Result!.Rank;
    }

    public Contender GetVisitedContender(string fullName)
    {
        throw new NotImplementedException();
    }

    public async Task Initialize(int newNumberOfAttempt)
    {
        _attemptNumber = newNumberOfAttempt;
    }
}