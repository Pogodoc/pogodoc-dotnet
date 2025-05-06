using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record GetTemplateIndexHtmlResponse
{
    [JsonPropertyName("templateIndex")]
    public required string TemplateIndex { get; init; }
}
