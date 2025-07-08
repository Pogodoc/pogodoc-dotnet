using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[Serializable]
public record SaveCreatedTemplateRequestTemplateInfo : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Title of the template
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Description of the template
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Type of template to be rendered
    /// </summary>
    [JsonPropertyName("type")]
    public required SaveCreatedTemplateRequestTemplateInfoType Type { get; set; }

    /// <summary>
    /// Sample data for the template
    /// </summary>
    [JsonPropertyName("sampleData")]
    public Dictionary<string, object?> SampleData { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("sourceCode")]
    public string? SourceCode { get; set; }

    /// <summary>
    /// Categories of the template
    /// </summary>
    [JsonPropertyName("categories")]
    public IEnumerable<SaveCreatedTemplateRequestTemplateInfoCategoriesItem> Categories { get; set; } =
        new List<SaveCreatedTemplateRequestTemplateInfoCategoriesItem>();

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
