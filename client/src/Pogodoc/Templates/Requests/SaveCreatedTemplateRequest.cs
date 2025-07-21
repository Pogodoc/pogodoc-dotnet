using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[Serializable]
public record SaveCreatedTemplateRequest
{
    [JsonPropertyName("templateInfo")]
    public required SaveCreatedTemplateRequestTemplateInfo TemplateInfo { get; set; }

    [JsonPropertyName("previewIds")]
    public required SaveCreatedTemplateRequestPreviewIds PreviewIds { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
