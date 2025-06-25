using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[Serializable]
public record GenerateTemplatePreviewsRequest
{
    /// <summary>
    /// Type of template to be rendered
    /// </summary>
    [JsonPropertyName("type")]
    public required GenerateTemplatePreviewsRequestType Type { get; set; }

    /// <summary>
    /// Sample data for the template
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, object?> Data { get; set; } = new Dictionary<string, object?>();

    /// <summary>
    /// Format options for the rendered document
    /// </summary>
    [JsonPropertyName("formatOpts")]
    public GenerateTemplatePreviewsRequestFormatOpts? FormatOpts { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
