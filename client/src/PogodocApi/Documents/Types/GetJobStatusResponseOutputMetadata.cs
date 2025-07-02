using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[Serializable]
public record GetJobStatusResponseOutputMetadata : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Time taken to render the output
    /// </summary>
    [JsonPropertyName("renderTime")]
    public required double RenderTime { get; set; }

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
