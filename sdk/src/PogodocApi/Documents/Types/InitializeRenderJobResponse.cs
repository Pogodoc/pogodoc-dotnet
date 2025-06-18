using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record InitializeRenderJobResponse
{
    /// <summary>
    /// ID of the render job
    /// </summary>
    [JsonPropertyName("jobId")]
    public required string JobId { get; set; }

    /// <summary>
    /// Type of output to be rendered
    /// </summary>
    [JsonPropertyName("target")]
    public required InitializeRenderJobResponseTarget Target { get; set; }

    /// <summary>
    /// Presigned URL to upload the data for the render job to S3
    /// </summary>
    [JsonPropertyName("presignedDataUploadUrl")]
    public string? PresignedDataUploadUrl { get; set; }

    /// <summary>
    /// Presigned URL to upload the template for the render job to S3. Only works with EJS templates
    /// </summary>
    [JsonPropertyName("presignedTemplateUploadUrl")]
    public string? PresignedTemplateUploadUrl { get; set; }

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
