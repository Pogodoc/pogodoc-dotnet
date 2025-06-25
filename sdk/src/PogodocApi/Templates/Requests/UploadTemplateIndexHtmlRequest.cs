using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[Serializable]
public record UploadTemplateIndexHtmlRequest
{
    /// <summary>
    /// New index.html file of the template
    /// </summary>
    [JsonPropertyName("indexHtml")]
    public required string IndexHtml { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
