using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[Serializable]
public record UpdateTemplateRequest
{
    [JsonPropertyName("templateInfo")]
    public UpdateTemplateRequestTemplateInfo? TemplateInfo { get; set; }

    [JsonPropertyName("previewIds")]
    public UpdateTemplateRequestPreviewIds? PreviewIds { get; set; }

    /// <summary>
    /// ID by which the new template content is saved
    /// </summary>
    [JsonPropertyName("contentId")]
    public string? ContentId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
