using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[Serializable]
public record GetUserTemplatesRequest
{
    /// <summary>
    /// Category of the template
    /// </summary>
    [JsonIgnore]
    public GetUserTemplatesRequestCategory? Category { get; set; }

    /// <summary>
    /// Search by title or description
    /// </summary>
    [JsonIgnore]
    public string? Search { get; set; }

    /// <summary>
    /// Type of template to be rendered
    /// </summary>
    [JsonIgnore]
    public GetUserTemplatesRequestType? Type { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    [JsonIgnore]
    public GetUserTemplatesRequestSort? Sort { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
