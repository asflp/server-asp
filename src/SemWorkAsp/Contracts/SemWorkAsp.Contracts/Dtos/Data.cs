using System.Text.Json.Serialization;

namespace SemWorkAsp.Contracts.Dtos;

public class Data
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("url")]
    public string URL { get; set; }
}