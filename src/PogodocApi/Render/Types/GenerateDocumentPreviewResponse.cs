using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record GenerateDocumentPreviewResponse
{
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("jobId")]
    public required string JobId { get; init; }
}
