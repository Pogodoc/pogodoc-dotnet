using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record SaveCreatedTemplateRequestTemplateInfo
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("type")]
    public required SaveCreatedTemplateRequestTemplateInfoType Type { get; set; }

    [JsonPropertyName("sampleData")]
    public object SampleData { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("sourceCode")]
    public string? SourceCode { get; set; }

    [JsonPropertyName("categories")]
    public IEnumerable<SaveCreatedTemplateRequestTemplateInfoCategoriesItem> Categories { get; set; } =
        new List<SaveCreatedTemplateRequestTemplateInfoCategoriesItem>();

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
