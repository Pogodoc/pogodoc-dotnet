using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record SaveCreatedTemplateRequest
{
    [JsonPropertyName("templateInfo")]
    public required SaveCreatedTemplateRequestTemplateInfo TemplateInfo { get; init; }

    [JsonPropertyName("previewIds")]
    public required SaveCreatedTemplateRequestPreviewIds PreviewIds { get; init; }
}
