using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record InitializeRenderJobRequest
{
    [JsonPropertyName("data")]
    public object? Data { get; set; }

    [JsonPropertyName("type")]
    public required InitializeRenderJobRequestType Type { get; set; }

    [JsonPropertyName("target")]
    public required InitializeRenderJobRequestTarget Target { get; set; }

    [JsonPropertyName("templateId")]
    public string? TemplateId { get; set; }

    [JsonPropertyName("formatOpts")]
    public InitializeRenderJobRequestFormatOpts? FormatOpts { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
