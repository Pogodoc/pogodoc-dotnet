using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record UpdateTemplateResponse
{
    [JsonPropertyName("newContentId")]
    public required string NewContentId { get; init; }
}
