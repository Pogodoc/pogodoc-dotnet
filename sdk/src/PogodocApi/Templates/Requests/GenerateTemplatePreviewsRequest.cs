using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record GenerateTemplatePreviewsRequest
{
    [JsonPropertyName("type")]
    public required GenerateTemplatePreviewsRequestType Type { get; set; }

    [JsonPropertyName("data")]
    public object Data { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("formatOpts")]
    public GenerateTemplatePreviewsRequestFormatOpts? FormatOpts { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
