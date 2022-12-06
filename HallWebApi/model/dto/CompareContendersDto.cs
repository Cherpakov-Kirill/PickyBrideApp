using System.Text.Json.Serialization;

namespace HallWebApi.model.dto;

public class CompareContendersDto
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; }
    
    [JsonPropertyName("secondName")]
    public string SecondName { get; }
 
    public CompareContendersDto(string firstName, string secondName)
    {
        FirstName = firstName;
        SecondName = secondName;
    } 
}