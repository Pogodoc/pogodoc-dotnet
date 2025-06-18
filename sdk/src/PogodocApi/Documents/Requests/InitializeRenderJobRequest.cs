using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record InitializeRenderJobRequest
{
    /// <summary>
    /// Sample data for the template
    /// </summary>
    [JsonPropertyName("data")]
    public object? Data { get; set; }

    /// <summary>
    /// Type of template to be rendered
    /// </summary>
    [JsonPropertyName("type")]
    public required InitializeRenderJobRequestType Type { get; set; }

    /// <summary>
    /// Type of output to be rendered
    /// </summary>
    [JsonPropertyName("target")]
    public required InitializeRenderJobRequestTarget Target { get; set; }

    /// <summary>
    /// ID of the template to be used
    /// </summary>
    [JsonPropertyName("templateId")]
    public string? TemplateId { get; set; }

    /// <summary>
    /// Format options for the rendered document
    /// </summary>
    [JsonPropertyName("formatOpts")]
    public InitializeRenderJobRequestFormatOpts? FormatOpts { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
