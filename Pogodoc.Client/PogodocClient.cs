namespace Pogodoc.Client;

using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Pogodoc.Client.Types;
using Pogodoc.Client.Utils;
using PogodocApi;
using PogodocApi.Core;

public class PogodocClient : PogodocApiClient
{
    public PogodocClient(string token, string? baseUrl)
        : base(token, new ClientOptions() { BaseUrl = baseUrl }) { }

    public async Task<string> SaveTemplateAsync(string path, SaveTemplateMetadata metadata)
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
        SaveTemplateMetadata metadata
    )
    {
        var initResponse = await PogodocClient.Templates.InitializeTemplateCreationAsync();

        await S3Utils.UploadToS3WithUrlAsync(
            initResponse.PresignedTemplateUploadUrl,
            payload,
            payloadLength,
            "application/zip"
        );

        await Templates.ExtractTemplateFilesAsync(initResponse.JobId);

        var previewsResponse = await Templates.GenerateTemplatePreviewsAsync(
            initResponse.JobId,
            new GenerateTemplatePreviewsRequest { Type = metadata.Type, Data = metadata.SampleData }
        );

        await Templates.SaveCreatedTemplateAsync(
            initResponse.JobId,
            new SaveCreatedTemplateRequest
            {
                TemplateInfo = new TemplateInfo
                {
                    Title = metadata.Title,
                    Description = metadata.Description,
                    Type = metadata.Type,
                    Categories = metadata.Categories,
                    SampleData = metadata.SampleData,
                    SourceCode = metadata.SourceCode,
                },
                PreviewIds = new PreviewIds
                {
                    PngJobId = previewsResponse.PngPreview.JobId,
                    PdfJobId = previewsResponse.PdfPreview.JobId,
                },
            }
        );

        return initResponse.JobId;
    }

    public async Task<string> UpdateTemplateAsync(string path, UpdateTemplateProps metadata)
    {
        using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            var fileInfo = new FileInfo(path);
            return await UpdateTemplateFromFileStreamAsync(fileStream, fileInfo.Length, metadata);
        }
    }

    public async Task<string> UpdateTemplateFromFileStreamAsync(
        Stream payload,
        long payloadLength,
        UpdateTemplateProps metadata
    )
    {
        var initResponse = await Templates.InitializeTemplateCreationAsync(); // This creates a new contentId

        await S3Utils.UploadToS3WithUrlAsync(
            initResponse.PresignedTemplateUploadUrl,
            payload,
            payloadLength,
            "application/zip"
        );

        await Templates.ExtractTemplateFilesAsync(initResponse.JobId); // Extracts files for the new contentId

        var previewsResponse = await Templates.GenerateTemplatePreviewsAsync(
            initResponse.JobId,
            new GenerateTemplatePreviewsRequest { Type = metadata.Type, Data = metadata.SampleData }
        );

        await Templates.UpdateTemplateAsync(
            metadata.TemplateId,
            new UpdateTemplateRequest // Pass the original templateId here
            {
                ContentId = initResponse.JobId, // This is the new contentId
                TemplateInfo = new TemplateInfoUpdate
                {
                    Title = metadata.Title,
                    Description = metadata.Description,
                    Type = metadata.Type,
                    Categories = metadata.Categories,
                    SampleData = metadata.SampleData,
                    SourceCode = metadata.SourceCode,
                },
                PreviewIds = new PreviewIds
                {
                    PngJobId = previewsResponse.PngPreview.JobId,
                    PdfJobId = previewsResponse.PdfPreview.JobId,
                },
            }
        );

        return initResponse.JobId; // Return the new contentId
    }

    public async Task<GetJobStatusResponse> GenerateDocumentAsync(GenerateDocumentProps props)
    {
        var initRequest = new InitializeRenderJobRequest
        {
            Type = props.RenderConfig.Type,
            Target = props.RenderConfig.Target,
            TemplateId = props.TemplateId,
            FormatOpts = props.RenderConfig.FormatOpts,
        };

        var initResponse = await Documents.InitializeRenderJobAsync(initRequest);

        var dataString = JsonSerializer.Serialize(props.Data);
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
                UploadPresignedS3Url = props.RenderConfig.PersonalUploadPresignedS3Url,
            }
        );

        var results = await Documents.GetJobStatusAsync(initResponse.JobId);
        return results;
    }
}
