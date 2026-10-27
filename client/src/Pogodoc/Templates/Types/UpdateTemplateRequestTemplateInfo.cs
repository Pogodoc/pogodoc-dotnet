using System.Text.Json;
using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[Serializable]
public record UpdateTemplateRequestTemplateInfo : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Title of the template
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Description of the template
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Type of template to be rendered
    /// </summary>
    [JsonPropertyName("type")]
    public UpdateTemplateRequestTemplateInfoType? Type { get; set; }

    /// <summary>
    /// Sample data for the template
    /// </summary>
    [JsonPropertyName("sampleData")]
    public Dictionary<string, object?>? SampleData { get; set; }

    [JsonPropertyName("sourceCode")]
    public string? SourceCode { get; set; }

    /// <summary>
    /// Categories of the template
    /// </summary>
    [JsonPropertyName("categories")]
    public IEnumerable<UpdateTemplateRequestTemplateInfoCategoriesItem>? Categories { get; set; }

    [JsonPropertyName("orientation")]
    public UpdateTemplateRequestTemplateInfoOrientation? Orientation { get; set; }

    [JsonPropertyName("dimensions")]
    public UpdateTemplateRequestTemplateInfoDimensions? Dimensions { get; set; }

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
