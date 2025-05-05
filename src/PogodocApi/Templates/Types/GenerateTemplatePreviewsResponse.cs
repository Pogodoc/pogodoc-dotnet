using System.Text.Json.Serialization;
using PogodocApi;

#nullable enable

namespace PogodocApi;

public record GenerateTemplatePreviewsResponse
{
    [JsonPropertyName("pngPreview")]
    public required GenerateTemplatePreviewsResponsePngPreview PngPreview { get; init; }

    [JsonPropertyName("pdfPreview")]
    public required GenerateTemplatePreviewsResponsePdfPreview PdfPreview { get; init; }
}
