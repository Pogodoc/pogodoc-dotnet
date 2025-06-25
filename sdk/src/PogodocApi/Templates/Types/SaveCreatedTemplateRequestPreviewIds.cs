using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[Serializable]
public record SaveCreatedTemplateRequestPreviewIds : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// ID of the job for the PNG preview
    /// </summary>
    [JsonPropertyName("pngJobId")]
    public required string PngJobId { get; set; }

    /// <summary>
    /// ID of the job for the PDF preview
    /// </summary>
    [JsonPropertyName("pdfJobId")]
    public required string PdfJobId { get; set; }

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
