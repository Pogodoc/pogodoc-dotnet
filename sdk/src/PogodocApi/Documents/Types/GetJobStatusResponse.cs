using System.Text.Json;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record GetJobStatusResponse
{
    [JsonPropertyName("type")]
    public required GetJobStatusResponseType Type { get; set; }

    [JsonPropertyName("jobId")]
    public required string JobId { get; set; }

    [JsonPropertyName("target")]
    public required GetJobStatusResponseTarget Target { get; set; }

    [JsonPropertyName("output")]
    public GetJobStatusResponseOutput? Output { get; set; }

    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

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
