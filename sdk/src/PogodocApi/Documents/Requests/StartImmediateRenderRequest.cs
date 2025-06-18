using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

public record StartImmediateRenderRequest
{
    /// <summary>
    /// Sample data for the template
    /// </summary>
    [JsonPropertyName("data")]
    public object StartImmediateRenderRequestData { get; set; } = new Dictionary<string, object?>();

    /// <summary>
    /// Type of template to be rendered
    /// </summary>
    [JsonPropertyName("type")]
    public required StartImmediateRenderRequestType Type { get; set; }

    /// <summary>
    /// Type of output to be rendered
    /// </summary>
    [JsonPropertyName("target")]
    public required StartImmediateRenderRequestTarget Target { get; set; }

    /// <summary>
    /// ID of the template to be used
    /// </summary>
    [JsonPropertyName("templateId")]
    public string? TemplateId { get; set; }

    /// <summary>
    /// Format options for the rendered document
    /// </summary>
    [JsonPropertyName("formatOpts")]
    public StartImmediateRenderRequestFormatOpts? FormatOpts { get; set; }

    /// <summary>
    /// index.html or ejs file of the template as a string
    /// </summary>
    [JsonPropertyName("template")]
    public string? Template { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
