using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record GenerateDocumentPreviewRequestFormatOpts
{
    [JsonPropertyName("fromPage")]
    public double? FromPage { get; init; }

    [JsonPropertyName("toPage")]
    public double? ToPage { get; init; }

    [JsonPropertyName("format")]
    public GenerateDocumentPreviewRequestFormatOptsFormat? Format { get; init; }

    [JsonPropertyName("waitForSelector")]
    public string? WaitForSelector { get; init; }
}
