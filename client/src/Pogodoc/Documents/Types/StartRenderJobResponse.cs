using System.Text.Json;
using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[Serializable]
public record StartRenderJobResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// ID of the render job
    /// </summary>
    [JsonPropertyName("jobId")]
    public required string JobId { get; set; }

    /// <summary>
    /// Type of output to be rendered
    /// </summary>
    [JsonPropertyName("target")]
    public StartRenderJobResponseTarget? Target { get; set; }

    /// <summary>
    /// Status of the render job
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Whether the render job was successful
    /// </summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    [JsonPropertyName("output")]
    public StartRenderJobResponseOutput? Output { get; set; }

    /// <summary>
    /// Error that occurred during render
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

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
