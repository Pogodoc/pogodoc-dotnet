using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record UpdateTemplateRequestTemplateInfo
{
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
    public required UpdateTemplateRequestTemplateInfoType Type { get; set; }

    /// <summary>
    /// Sample data for the template
    /// </summary>
    [JsonPropertyName("sampleData")]
    public object SampleData { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("sourceCode")]
    public string? SourceCode { get; set; }

    /// <summary>
    /// Categories of the template
    /// </summary>
    [JsonPropertyName("categories")]
    public IEnumerable<UpdateTemplateRequestTemplateInfoCategoriesItem> Categories { get; set; } =
        new List<UpdateTemplateRequestTemplateInfoCategoriesItem>();

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
