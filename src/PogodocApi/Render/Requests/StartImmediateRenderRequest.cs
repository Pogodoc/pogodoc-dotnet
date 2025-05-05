using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record StartImmediateRenderRequest
{
    [JsonPropertyName("data")]
    public Dictionary<string, object> StartImmediateRenderRequestData { get; init; } =
        new Dictionary<string, object>();

    [JsonPropertyName("type")]
    public required StartImmediateRenderRequestType Type { get; init; }

    [JsonPropertyName("target")]
    public required StartImmediateRenderRequestTarget Target { get; init; }

    [JsonPropertyName("templateId")]
    public string? TemplateId { get; init; }

    [JsonPropertyName("formatOpts")]
    public StartImmediateRenderRequestFormatOpts? FormatOpts { get; init; }

    [JsonPropertyName("template")]
    public string? Template { get; init; }
}
