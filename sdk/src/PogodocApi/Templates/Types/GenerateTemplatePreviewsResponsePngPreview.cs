using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record GenerateTemplatePreviewsResponsePngPreview
{
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
