using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record CloneTemplateResponse
{
    [JsonPropertyName("newTemplateId")]
    public required string NewTemplateId { get; init; }
}
