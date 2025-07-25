using System.Text.Json;
using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[Serializable]
public record GenerateTemplatePreviewsResponsePdfPreview : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// URL of the rendered preview
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    /// <summary>
    /// ID of the render job
    /// </summary>
    [JsonPropertyName("jobId")]
    public required string JobId { get; set; }

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
