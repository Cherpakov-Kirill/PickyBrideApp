using System.Text.Json.Serialization;

namespace HallWebApi.model.dto;

public class ContenderNameDto
{
    [JsonPropertyName("name")]
    public string? Name { get; }
 
    public ContenderNameDto(string? name)
    {
        Name = name;
    } 
}