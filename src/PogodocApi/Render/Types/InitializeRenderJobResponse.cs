using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record InitializeRenderJobResponse
{
    [JsonPropertyName("jobId")]
    public required string JobId { get; init; }

    [JsonPropertyName("target")]
    public required InitializeRenderJobResponseTarget Target { get; init; }

    [JsonPropertyName("presignedDataUploadUrl")]
    public string? PresignedDataUploadUrl { get; init; }

    [JsonPropertyName("presignedTemplateUploadUrl")]
    public string? PresignedTemplateUploadUrl { get; init; }
}
