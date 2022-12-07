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

    public async Task<string?> LetTheNextContenderGoToThePrincess()
    {
        var response = await _httpController.SendPostRequest<ContenderNameDto>($"/hall/{_attemptNumber}/next");
        return response!.Name;
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