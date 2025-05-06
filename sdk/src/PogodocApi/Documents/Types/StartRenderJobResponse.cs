using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record StartRenderJobResponse
{
    [JsonPropertyName("jobId")]
    public required string JobId { get; init; }
}
