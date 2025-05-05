using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record GetJobStatusResponseOutputMetadata
{
    [JsonPropertyName("renderTime")]
    public required double RenderTime { get; init; }
}
