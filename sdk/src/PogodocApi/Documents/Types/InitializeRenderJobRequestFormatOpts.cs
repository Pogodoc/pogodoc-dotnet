using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

/// <summary>
/// Format options for the rendered document
/// </summary>
public record InitializeRenderJobRequestFormatOpts
{
    [JsonPropertyName("fromPage")]
    public double? FromPage { get; set; }

    [JsonPropertyName("toPage")]
    public double? ToPage { get; set; }

    [JsonPropertyName("format")]
    public InitializeRenderJobRequestFormatOptsFormat? Format { get; set; }

    /// <summary>
    /// Selector to wait for to know when the page is loaded and can be saved as pdf, png, etc.
    /// </summary>
    [JsonPropertyName("waitForSelector")]
    public string? WaitForSelector { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
