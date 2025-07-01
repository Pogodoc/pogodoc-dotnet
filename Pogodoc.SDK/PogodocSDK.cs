namespace Pogodoc.SDK;

using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Pogodoc.SDK.Types;
using Pogodoc.SDK.Utils;
using PogodocApi;
using PogodocApi.Core;
using DotNetEnv;

public class PogodocSDK : PogodocApiClient
{

    public PogodocSDK(string? token = null, string? baseUrl = null)
        : base(ResolveToken(token), new ClientOptions(){ BaseUrl = ResolveBaseUrl(baseUrl)})
    {}

    private static string ResolveToken(string? token)
    {
        Env.Load();
        return token ?? Environment.GetEnvironmentVariable("API_TOKEN")
               ?? throw new ArgumentException("API token is required. Please provide it either as a parameter or set the API_TOKEN environment variable.");
    }

    private static string ResolveBaseUrl(string? baseUrl)
    {
        Env.Load();
        return baseUrl ?? Environment.GetEnvironmentVariable("BASE_URL") ?? PogodocApiEnvironment.Default;
    }

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
                Type = Enum.Parse<GenerateTemplatePreviewsRequestType>(metadata.Type.ToString()),
                Data = metadata.SampleData,
            }
        );

        await Templates.SaveCreatedTemplateAsync(
            initResponse.TemplateId,
            new SaveCreatedTemplateRequest
            {
                TemplateInfo = metadata,
                PreviewIds = new SaveCreatedTemplateRequestPreviewIds
                {
                    PngJobId = previewsResponse.PngPreview.JobId,
                    PdfJobId = previewsResponse.PdfPreview.JobId,
                },
            }
        );

        return initResponse.TemplateId;
    }

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
                Type = Enum.Parse<GenerateTemplatePreviewsRequestType>(metadata.Type.ToString()),
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

    public async Task<GetJobStatusResponse> GenerateDocumentAsync(GenerateDocumentProps props)
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

        await Documents.StartRenderJobAsync(
            initResponse.JobId,
            new StartRenderJobRequest
            {
                ShouldWaitForRenderCompletion = props.ShouldWaitForRenderCompletion,
                UploadPresignedS3Url = props.UploadPresignedS3Url,
            }
        );

        var results = await Documents.GetJobStatusAsync(initResponse.JobId);
        return results;
    }
}
