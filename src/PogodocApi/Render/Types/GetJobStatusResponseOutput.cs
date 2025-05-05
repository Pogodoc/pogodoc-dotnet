using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record GetJobStatusResponseOutput
{
    [JsonPropertyName("data")]
    public required GetJobStatusResponseOutputData Data { get; init; }

    [JsonPropertyName("metadata")]
    public required GetJobStatusResponseOutputMetadata Metadata { get; init; }
}
