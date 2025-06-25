using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

/// <summary>
/// Format options for the rendered document
/// </summary>
[Serializable]
public record GenerateTemplatePreviewsRequestFormatOpts : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("fromPage")]
    public double? FromPage { get; set; }

    [JsonPropertyName("toPage")]
    public double? ToPage { get; set; }

    [JsonPropertyName("format")]
    public GenerateTemplatePreviewsRequestFormatOptsFormat? Format { get; set; }

    /// <summary>
    /// Selector to wait for to know when the page is loaded and can be saved as pdf, png, etc.
    /// </summary>
    [JsonPropertyName("waitForSelector")]
    public string? WaitForSelector { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
