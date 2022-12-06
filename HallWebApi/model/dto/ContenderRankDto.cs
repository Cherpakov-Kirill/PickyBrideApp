using System.Text.Json.Serialization;

namespace HallWebApi.model.dto;

public class ContenderRankDto
{
    [JsonPropertyName("rank")]
    public int Rank { get; }
 
    public ContenderRankDto(int rank)
    {
        Rank = rank;
    } 
}