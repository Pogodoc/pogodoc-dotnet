using PogodocApi;

#nullable enable

namespace PogodocApi;

public record GenerateDocumentPreviewRequest
{
    public required string TemplateId { get; init; }

    public required GenerateDocumentPreviewRequestType Type { get; init; }

    public Dictionary<string, object> Data { get; init; } = new Dictionary<string, object>();

    public GenerateDocumentPreviewRequestFormatOpts? FormatOpts { get; init; }
}
