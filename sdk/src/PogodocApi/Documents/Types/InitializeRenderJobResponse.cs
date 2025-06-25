using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[Serializable]
public record InitializeRenderJobResponse : IJsonOnDeserialized
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
