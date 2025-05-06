using System.Text.Json.Serialization;

#nullable enable

namespace PogodocApi;

public record SaveCreatedTemplateRequestPreviewIds
{
    [JsonPropertyName("pngJobId")]
    public required string PngJobId { get; init; }

    [JsonPropertyName("pdfJobId")]
    public required string PdfJobId { get; init; }
}
