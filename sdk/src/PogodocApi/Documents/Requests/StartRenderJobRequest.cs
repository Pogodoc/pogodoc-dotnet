using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[Serializable]
public record StartRenderJobRequest
{
    /// <summary>
    /// Whether to wait for the render job to complete, if false, the job will be returned immediately
    /// </summary>
    [JsonPropertyName("shouldWaitForRenderCompletion")]
    public bool? ShouldWaitForRenderCompletion { get; set; }

    [JsonPropertyName("uploadPresignedS3Url")]
    public string? UploadPresignedS3Url { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
