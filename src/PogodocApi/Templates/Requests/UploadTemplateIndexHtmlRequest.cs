using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record UploadTemplateIndexHtmlRequest
{
    [JsonPropertyName("templateIndex")]
    public required string TemplateIndex { get; init; }
}
