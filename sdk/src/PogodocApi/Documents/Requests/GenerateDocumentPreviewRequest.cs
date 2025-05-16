using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record GenerateDocumentPreviewRequest
{
    [JsonIgnore]
    public required string TemplateId { get; set; }

    [JsonPropertyName("type")]
    public required GenerateDocumentPreviewRequestType Type { get; set; }

    [JsonPropertyName("data")]
    public object Data { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("formatOpts")]
    public GenerateDocumentPreviewRequestFormatOpts? FormatOpts { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
