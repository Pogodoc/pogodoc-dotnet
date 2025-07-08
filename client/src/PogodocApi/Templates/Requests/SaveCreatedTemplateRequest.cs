using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

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
