using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record StartImmediateRenderRequest
{
    [JsonPropertyName("data")]
    public object StartImmediateRenderRequestData { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("type")]
    public required StartImmediateRenderRequestType Type { get; set; }

    [JsonPropertyName("target")]
    public required StartImmediateRenderRequestTarget Target { get; set; }

    [JsonPropertyName("templateId")]
    public string? TemplateId { get; set; }

    [JsonPropertyName("formatOpts")]
    public StartImmediateRenderRequestFormatOpts? FormatOpts { get; set; }

    [JsonPropertyName("template")]
    public string? Template { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
