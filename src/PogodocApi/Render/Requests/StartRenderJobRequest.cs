using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record StartRenderJobRequest
{
    [JsonPropertyName("shouldWaitForRenderCompletion")]
    public bool? ShouldWaitForRenderCompletion { get; init; }

    [JsonPropertyName("uploadPresignedS3Url")]
    public string? UploadPresignedS3Url { get; init; }
}
