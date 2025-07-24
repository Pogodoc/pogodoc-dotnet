using System.Runtime.CompilerServices;
using Pogodoc;

namespace Pogodoc.Types;

/// <summary>
/// Represents the properties for generating a document.
/// </summary>
public class GenerateDocumentProps
{
    /// <summary>
    /// The render configuration.
    /// </summary>
    public required InitializeRenderJobRequest RenderConfig { get; set; }

    /// <summary>
    /// Optional template string to use for generating the document.
    /// </summary>
    public string? Template { get; set; }

    /// <summary>
    /// Optional presigned S3 URL to upload the data to.
    /// </summary>
    public string? UploadPresignedS3Url { get; set; }
}

/// <summary>
/// Represents the properties for generating a document immediately.
/// </summary>
public class ImmediateGenerateDocumentProps
{
    /// <summary>
    /// The render configuration.
    /// </summary>
    public required StartImmediateRenderRequest RenderConfig { get; set; }

    /// <summary>
    /// Optional template string to use for generating the document.
    /// </summary>
    public string? Template { get; set; }
}
