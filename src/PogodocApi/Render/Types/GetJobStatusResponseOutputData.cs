using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record GetJobStatusResponseOutputData
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }
}
