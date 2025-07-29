namespace Pogodoc;

using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Pogodoc;
using Pogodoc.Core;
using Pogodoc.Types;
using Pogodoc.Utils;

/// <summary>
/// Provides a high-level interface to the Pogodoc API, simplifying template management and document generation.
/// </summary>
public class PogodocSDK : PogodocApiClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PogodocSDK"/> class.
    /// </summary>
    /// <param name="token">The API token. If not provided, it will be read from the POGODOC_API_TOKEN environment variable.</param>
    /// <param name="baseUrl">The base URL for the Pogodoc API. If not provided, it will be read from the POGODOC_BASE_URL environment variable or default to the production environment.</param>
    /// <exception cref="ArgumentException">Thrown if the API token is not provided via parameter or environment variable.</exception>
    public PogodocSDK(string? token = null, string? baseUrl = null)
        : base(ResolveToken(token), new ClientOptions() { BaseUrl = ResolveBaseUrl(baseUrl) }) { }

    private static string ResolveToken(string? token)
    {
        return token
            ?? Environment.GetEnvironmentVariable("POGODOC_API_TOKEN")
            ?? throw new ArgumentException(
                "API token is required. Please provide it either as a parameter or set the POGODOC_API_TOKEN environment variable."
            );
    }

    private static string ResolveBaseUrl(string? baseUrl)
    {
        return baseUrl
            ?? Environment.GetEnvironmentVariable("POGODOC_BASE_URL")
            ?? PogodocApiEnvironment.Default;
    }

    /// <summary>
    /// Saves a new template from a local file path.
    /// </summary>
    /// <remarks>
    /// This method reads a template from a .zip file, uploads it, and saves it in Pogodoc.
    /// It is a convenient wrapper around <see cref="SaveTemplateFromFileStreamAsync"/>.
    /// </remarks>
    /// <param name="path">The local file path to the .zip file containing the template.</param>
    /// <param name="metadata">The metadata for the template. See <see cref="SaveCreatedTemplateRequestTemplateInfo"/> for more details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new template's ID.</returns>
    public async Task<string> SaveTemplateAsync(
        string path,
        SaveCreatedTemplateRequestTemplateInfo metadata
    )
    {
        using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            var fileInfo = new FileInfo(path);
            return await SaveTemplateFromFileStreamAsync(fileStream, fileInfo.Length, metadata);
        }
    }

    /// <summary>
    /// Saves a new template from a file stream.
    /// </summary>
    /// <remarks>
    /// This is the core method for creating templates. It uploads a template from a stream,
    /// generates previews, and saves it with the provided metadata.
    /// </remarks>
    /// <param name="payload">The readable stream of the .zip file.</param>
    /// <param name="payloadLength">The length of the payload in bytes.</param>
    /// <param name="metadata">The metadata for the template. See <see cref="SaveCreatedTemplateRequestTemplateInfo"/> for more details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new template's ID.</returns>
    public async Task<string> SaveTemplateFromFileStreamAsync(
        Stream payload,
        long payloadLength,
        SaveCreatedTemplateRequestTemplateInfo metadata
    )
    {
        var initResponse = await Templates.InitializeTemplateCreationAsync();

        await S3Utils.UploadToS3WithUrlAsync(
            initResponse.PresignedTemplateUploadUrl,
            payload,
            payloadLength,
            "application/zip"
        );

        await Templates.ExtractTemplateFilesAsync(initResponse.TemplateId, null);

        var previewsResponse = await Templates.GenerateTemplatePreviewsAsync(
            initResponse.TemplateId,
            new GenerateTemplatePreviewsRequest
            {
                Type = GenerateTemplatePreviewsRequestType.FromCustom(metadata.Type.ToString()),
                Data = metadata.SampleData,
            }
        );

        await Templates.SaveCreatedTemplateAsync(
            initResponse.TemplateId,
            new SaveCreatedTemplateRequest
            {
                TemplateInfo = new SaveCreatedTemplateRequestTemplateInfo
                {
                    Title = metadata.Title,
                    Description = metadata.Description,
                    Type = metadata.Type,
                    Categories = metadata.Categories,
                    SampleData = metadata.SampleData,
                    SourceCode = metadata.SourceCode,
                },
                PreviewIds = new SaveCreatedTemplateRequestPreviewIds
                {
                    PngJobId = previewsResponse.PngPreview.JobId,
                    PdfJobId = previewsResponse.PdfPreview.JobId,
                },
            }
        );

        return initResponse.TemplateId;
    }

    /// <summary>
    /// Updates an existing template from a local file path.
    /// </summary>
    /// <remarks>
    /// This method reads a new version of a template from a .zip file, uploads it,
    /// and updates the existing template in Pogodoc.
    /// It is a convenient wrapper around <see cref="UpdateTemplateFromFileStreamAsync"/>.
    /// </remarks>
    /// <param name="path">The local file path to the .zip file containing the new template version.</param>
    /// <param name="templateId">The ID of the template to update.</param>
    /// <param name="metadata">The new metadata for the template. See <see cref="UpdateTemplateRequestTemplateInfo"/> for more details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the content ID of the new template version.</returns>
    public async Task<string> UpdateTemplateAsync(
        string path,
        string templateId,
        UpdateTemplateRequestTemplateInfo metadata
    )
    {
        using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            var fileInfo = new FileInfo(path);
            return await UpdateTemplateFromFileStreamAsync(
                fileStream,
                fileInfo.Length,
                templateId,
                metadata
            );
        }
    }

    /// <summary>
    /// Updates an existing template from a file stream.
    /// </summary>
    /// <remarks>
    /// This is the core method for updating templates. It uploads a new template version from a stream,
    /// generates new previews, and updates the template with the provided metadata.
    /// </remarks>
    /// <param name="payload">The readable stream of the .zip file with the new template version.</param>
    /// <param name="payloadLength">The length of the payload in bytes.</param>
    /// <param name="templateId">The ID of the template to update.</param>
    /// <param name="metadata">The new metadata for the template. See <see cref="UpdateTemplateRequestTemplateInfo"/> for more details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the content ID of the new template version.</returns>
    public async Task<string> UpdateTemplateFromFileStreamAsync(
        Stream payload,
        long payloadLength,
        string templateId,
        UpdateTemplateRequestTemplateInfo metadata
    )
    {
        var initResponse = await Templates.InitializeTemplateCreationAsync();

        var contentId = initResponse.TemplateId;

        await S3Utils.UploadToS3WithUrlAsync(
            initResponse.PresignedTemplateUploadUrl,
            payload,
            payloadLength,
            "application/zip"
        );

        await Templates.ExtractTemplateFilesAsync(contentId, null);

        var previewsResponse = await Templates.GenerateTemplatePreviewsAsync(
            contentId,
            new GenerateTemplatePreviewsRequest
            {
                Type = GenerateTemplatePreviewsRequestType.FromCustom(metadata.Type.ToString()),
                Data = metadata.SampleData,
            }
        );

        await Templates.UpdateTemplateAsync(
            templateId,
            new UpdateTemplateRequest
            {
                ContentId = contentId,
                TemplateInfo = new UpdateTemplateRequestTemplateInfo
                {
                    Title = metadata.Title,
                    Description = metadata.Description,
                    Type = metadata.Type,
                    Categories = metadata.Categories,
                    SampleData = metadata.SampleData,
                    SourceCode = metadata.SourceCode,
                },
                PreviewIds = new UpdateTemplateRequestPreviewIds
                {
                    PngJobId = previewsResponse.PngPreview.JobId,
                    PdfJobId = previewsResponse.PdfPreview.JobId,
                },
            }
        );

        return contentId;
    }

    /// <summary>
    /// Generates a document and returns the result immediately.
    /// </summary>
    /// <param name="props">The properties for generating a document. See <see cref="ImmediateGenerateDocumentProps"/> for more details.</param>
    /// <remarks>
    /// Use this method for quick, synchronous rendering of small documents.
    /// The result is returned directly in the response.
    /// For larger documents or when you need to handle rendering asynchronously, use <see cref="GenerateDocumentAsync"/>.
    /// You must provide either a `templateId` of a saved template or a `template` string in the <paramref name="props"/>.
    /// </remarks>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the presigned url of the generated document.
    /// </returns>
    public async Task<StartImmediateRenderResponse> GenerateDocumentImmediateAsync(
        ImmediateGenerateDocumentProps props
    )
    {
        return await Documents.StartImmediateRenderAsync(
            new StartImmediateRenderRequest
            {
                Template = props.Template,
                TemplateId = props.RenderConfig.TemplateId,
                Type = props.RenderConfig.Type,
                Target = props.RenderConfig.Target,
                FormatOpts = props.RenderConfig.FormatOpts,
                Data = props.RenderConfig.Data,
            }
        );
    }

    /// <summary>
    /// Starts an asynchronous document generation job.
    /// </summary>
    /// <remarks>
    /// This is a lower-level method that only initializes the job. It returns the initial job information, including the `jobId`.
    /// Use <see cref="PollForJobCompletionAsync"/> with the `jobId` to get the final result, or use the higher-level <see cref="GenerateDocumentAsync"/> to handle the process automatically.
    /// <para>You must provide either a `templateId` of a saved template or a `template` string in the <paramref name="props"/>.</para>
    /// </remarks>
    /// <param name="props">The properties for generating a document.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the initial job information.</returns>
    public async Task<StartRenderJobResponse> StartGenerateDocumentAsync(
        GenerateDocumentProps props
    )
    {
        var initRequest = new InitializeRenderJobRequest
        {
            Type = props.RenderConfig.Type,
            Target = props.RenderConfig.Target,
            TemplateId = props.RenderConfig.TemplateId,
            FormatOpts = props.RenderConfig.FormatOpts,
        };

        var initResponse = await Documents.InitializeRenderJobAsync(initRequest);

        var dataString = JsonSerializer.Serialize(props.RenderConfig.Data);
        using (var dataStream = new MemoryStream(Encoding.UTF8.GetBytes(dataString)))
        {
            if (!string.IsNullOrEmpty(initResponse.PresignedDataUploadUrl))
            {
                await S3Utils.UploadToS3WithUrlAsync(
                    initResponse.PresignedDataUploadUrl,
                    dataStream,
                    dataStream.Length,
                    "application/json"
                );
            }
        }

        if (
            !string.IsNullOrEmpty(props.Template)
            && !string.IsNullOrEmpty(initResponse.PresignedTemplateUploadUrl)
        )
        {
            using (var templateStream = new MemoryStream(Encoding.UTF8.GetBytes(props.Template)))
            {
                await S3Utils.UploadToS3WithUrlAsync(
                    initResponse.PresignedTemplateUploadUrl,
                    templateStream,
                    templateStream.Length,
                    "text/html"
                );
            }
        }

        return await Documents.StartRenderJobAsync(
            initResponse.JobId,
            new StartRenderJobRequest { UploadPresignedS3Url = props.UploadPresignedS3Url }
        );
    }

    /// <summary>
    /// Generates a document by starting a job and polling for its completion.
    /// </summary>
    /// <remarks>
    /// This is the recommended method for most use cases, especially for larger documents.
    /// It calls <see cref="StartGenerateDocumentAsync"/> to begin the process, then <see cref="PollForJobCompletionAsync"/> to wait for the result.
    /// <para>You must provide either a `templateId` of a saved template or a `template` string in the <paramref name="props"/>.</para>
    /// </remarks>
    /// <param name="props">The properties for generating a document.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the final job status, including the output URL.</returns>
    public async Task<GetJobStatusResponse> GenerateDocumentAsync(GenerateDocumentProps props)
    {
        var initResponse = await StartGenerateDocumentAsync(props);

        return await PollForJobCompletionAsync(initResponse.JobId);
    }

    /// <summary>
    /// Polls for the completion of a rendering job.
    /// </summary>
    /// <remarks>
    /// This method repeatedly checks the status of a job until it is complete.
    /// </remarks>
    /// <param name="jobId">The ID of the job to poll.</param>
    /// <param name="maxAttempts">The maximum number of polling attempts.</param>
    /// <param name="intervalMs">The interval in milliseconds between polling attempts.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the final job status.</returns>
    public async Task<GetJobStatusResponse> PollForJobCompletionAsync(
        string jobId,
        int maxAttempts = 60,
        int intervalMs = 500
    )
    {
        await Task.Delay(1000);

        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            var job = await Documents.GetJobStatusAsync(jobId);

            if (job.Status == "done")
            {
                return job;
            }

            await Task.Delay(intervalMs);
        }

        return await Documents.GetJobStatusAsync(jobId);
    }
}
