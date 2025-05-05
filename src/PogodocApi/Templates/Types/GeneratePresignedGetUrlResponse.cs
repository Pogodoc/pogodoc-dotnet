using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record GeneratePresignedGetUrlResponse
{
    [JsonPropertyName("presignedUrl")]
    public required string PresignedUrl { get; init; }
}
