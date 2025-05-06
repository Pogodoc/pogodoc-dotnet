using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record GetJobStatusResponse
{
    [JsonPropertyName("type")]
    public required GetJobStatusResponseType Type { get; init; }

    [JsonPropertyName("jobId")]
    public required string JobId { get; init; }

    [JsonPropertyName("target")]
    public required GetJobStatusResponseTarget Target { get; init; }

    [JsonPropertyName("output")]
    public GetJobStatusResponseOutput? Output { get; init; }

    [JsonPropertyName("success")]
    public bool? Success { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
