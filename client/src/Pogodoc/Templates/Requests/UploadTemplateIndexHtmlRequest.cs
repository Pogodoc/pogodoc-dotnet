using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

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
