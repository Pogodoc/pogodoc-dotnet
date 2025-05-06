using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record UpdateTemplateRequestTemplateInfo
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("type")]
    public required UpdateTemplateRequestTemplateInfoType Type { get; init; }

    [JsonPropertyName("sampleData")]
    public Dictionary<string, object> SampleData { get; init; } = new Dictionary<string, object>();

    [JsonPropertyName("sourceCode")]
    public string? SourceCode { get; init; }

    [JsonPropertyName("categories")]
    public IEnumerable<UpdateTemplateRequestTemplateInfoCategoriesItem> Categories { get; init; } =
        new List<UpdateTemplateRequestTemplateInfoCategoriesItem>();
}
