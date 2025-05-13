namespace Pogodoc.Client.Types;

// Helper classes to represent the structure from TypeScript
public class SaveTemplateMetadata
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }
    public string[]? Categories { get; set; }
    public object? SampleData { get; set; }
    public string? SourceCode { get; set; }
}

public class UpdateTemplateProps : SaveTemplateMetadata
{
    public string TemplateId { get; set; }
}

public class GenerateDocumentProps
{
    public string? Template { get; set; } // string content of template
    public string TemplateId { get; set; }
    public object Data { get; set; }
    public RenderConfig RenderConfig { get; set; }
    public bool? ShouldWaitForRenderCompletion { get; set; }
}

public class RenderConfig
{
    public string Type { get; set; }
    public string Target { get; set; }
    public object? FormatOpts { get; set; }
    public string? PersonalUploadPresignedS3Url { get; set; }
}
