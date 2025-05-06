using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record GenerateTemplatePreviewsRequest
{
    [JsonPropertyName("type")]
    public required GenerateTemplatePreviewsRequestType Type { get; init; }

    [JsonPropertyName("data")]
    public Dictionary<string, object> Data { get; init; } = new Dictionary<string, object>();

    [JsonPropertyName("formatOpts")]
    public GenerateTemplatePreviewsRequestFormatOpts? FormatOpts { get; init; }
}
