using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record UpdateTemplateRequest
{
    [JsonPropertyName("templateInfo")]
    public required UpdateTemplateRequestTemplateInfo TemplateInfo { get; init; }

    [JsonPropertyName("previewIds")]
    public required UpdateTemplateRequestPreviewIds PreviewIds { get; init; }

    [JsonPropertyName("contentId")]
    public required string ContentId { get; init; }
}
