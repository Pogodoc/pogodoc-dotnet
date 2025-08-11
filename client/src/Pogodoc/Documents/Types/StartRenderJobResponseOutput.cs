using System.Text.Json;
using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[Serializable]
public record StartRenderJobResponseOutput : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("data")]
    public required StartRenderJobResponseOutputData Data { get; set; }

    [JsonPropertyName("metadata")]
    public required StartRenderJobResponseOutputMetadata Metadata { get; set; }

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
