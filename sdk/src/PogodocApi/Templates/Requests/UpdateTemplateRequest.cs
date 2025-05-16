using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record UpdateTemplateRequest
{
    [JsonPropertyName("templateInfo")]
    public required UpdateTemplateRequestTemplateInfo TemplateInfo { get; set; }

    [JsonPropertyName("previewIds")]
    public required UpdateTemplateRequestPreviewIds PreviewIds { get; set; }

    [JsonPropertyName("contentId")]
    public required string ContentId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
