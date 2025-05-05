using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record InitializeRenderJobRequest
{
    [JsonPropertyName("data")]
    public Dictionary<string, object>? Data { get; init; }

    [JsonPropertyName("type")]
    public required InitializeRenderJobRequestType Type { get; init; }

    [JsonPropertyName("target")]
    public required InitializeRenderJobRequestTarget Target { get; init; }

    [JsonPropertyName("templateId")]
    public string? TemplateId { get; init; }

    [JsonPropertyName("formatOpts")]
    public InitializeRenderJobRequestFormatOpts? FormatOpts { get; init; }
}
