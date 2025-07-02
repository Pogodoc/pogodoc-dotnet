using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[Serializable]
public record GenerateTemplatePreviewsResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("pngPreview")]
    public required GenerateTemplatePreviewsResponsePngPreview PngPreview { get; set; }

    [JsonPropertyName("pdfPreview")]
    public required GenerateTemplatePreviewsResponsePdfPreview PdfPreview { get; set; }

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
