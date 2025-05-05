using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record GenerateTemplatePreviewsResponsePngPreview
{
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("jobId")]
    public required string JobId { get; init; }
}
