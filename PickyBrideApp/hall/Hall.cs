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

    public Task LetTheNextContenderGoToThePrincess()
    {
        return _httpController.SendPostRequest($"/hall/{_attemptNumber}/next");
    }

    public async Task<int> SelectContender()
    {
        var response = await _httpController.SendPostRequest<ContenderRankDto>($"/hall/{_attemptNumber}/select");
        return response!.Rank;
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