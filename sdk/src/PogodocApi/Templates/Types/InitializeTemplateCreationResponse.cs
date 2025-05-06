using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record InitializeTemplateCreationResponse
{
    [JsonPropertyName("jobId")]
    public required string JobId { get; init; }

    [JsonPropertyName("presignedTemplateUploadUrl")]
    public required string PresignedTemplateUploadUrl { get; init; }
}
