using System.Runtime.CompilerServices;
using Pogodoc;

namespace Pogodoc.Types;

// Helper classes to represent the structure from TypeScript

public class GenerateDocumentProps
{
    public required InitializeRenderJobRequest RenderConfig { get; set; }
    public string? Template { get; set; }
    public string? UploadPresignedS3Url { get; set; }
}

public class ImmediateGenerateDocumentProps
{
    public required StartImmediateRenderRequest RenderConfig { get; set; }
    public string? Template { get; set; }
}
