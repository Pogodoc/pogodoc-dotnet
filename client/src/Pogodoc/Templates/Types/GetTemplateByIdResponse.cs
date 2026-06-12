using System.Text.Json;
using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[Serializable]
public record GetTemplateByIdResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique ID of the template
    /// </summary>
    [JsonPropertyName("uuid")]
    public required string Uuid { get; set; }

    /// <summary>
    /// Title of the template
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Description of the template
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Type of template to be rendered
    /// </summary>
    [JsonPropertyName("type")]
    public required GetTemplateByIdResponseType Type { get; set; }

    /// <summary>
    /// Categories of the template
    /// </summary>
    [JsonPropertyName("categories")]
    public IEnumerable<string> Categories { get; set; } = new List<string>();

    /// <summary>
    /// Permissions of the template
    /// </summary>
    [JsonPropertyName("permissions")]
    public required GetTemplateByIdResponsePermissions Permissions { get; set; }

    /// <summary>
    /// Preview URL of the template
    /// </summary>
    [JsonPropertyName("preview")]
    public string? Preview { get; set; }

    /// <summary>
    /// Content ID of the template in S3
    /// </summary>
    [JsonPropertyName("contentId")]
    public required string ContentId { get; set; }

    /// <summary>
    /// Source code of the template
    /// </summary>
    [JsonPropertyName("sourceCode")]
    public string? SourceCode { get; set; }

    /// <summary>
    /// Sample data for the template
    /// </summary>
    [JsonPropertyName("sampleData")]
    public Dictionary<string, object?>? SampleData { get; set; }

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
