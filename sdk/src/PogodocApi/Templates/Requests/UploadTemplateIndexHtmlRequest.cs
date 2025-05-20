using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record UploadTemplateIndexHtmlRequest
{
    [JsonPropertyName("templateIndex")]
    public required string TemplateIndex { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
