using System.Net.Http;
using System.Text.Json;
using PogodocApi;
using PogodocApi.Core;

#nullable enable

namespace PogodocApi;

public class TemplatesClient
{
    private RawClient _client;

    public TemplatesClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Initializes template creation by generating a unique ID and providing a presigned URL for template ZIP upload. Sets unfinished tag for tracking incomplete templates.
    /// </summary>
    public async Task<InitializeTemplateCreationResponse> InitializeTemplateCreationAsync()
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = "templates/init" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<InitializeTemplateCreationResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Finalizes template creation by saving template info to Strapi, copying preview files to permanent storage, and creating template index. Removes unfinished tag upon completion.
    /// </summary>
    public async Task SaveCreatedTemplateAsync(
        string templateId,
        SaveCreatedTemplateRequest request
    )
    {
        await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = $"templates/{templateId}",
                Body = request
            }
        );
    }

    /// <summary>
    /// Updates template content, handles S3 storage cleanup for old content, updates template metadata in Strapi, and manages preview files. Removes unfinished tags after successful update.
    /// </summary>
    public async Task<UpdateTemplateResponse> UpdateTemplateAsync(
        string templateId,
        UpdateTemplateRequest request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Put,
                Path = $"templates/{templateId}",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<UpdateTemplateResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Deletes a template from Strapi and associated S3 storage. Removes all associated files and metadata.
    /// </summary>
    public async Task DeleteTemplateAsync(string templateId)
    {
        await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"templates/{templateId}"
            }
        );
    }

    /// <summary>
    /// Extracts contents from an uploaded template ZIP file and stores individual files in the appropriate S3 storage structure.
    /// </summary>
    public async Task ExtractTemplateFilesAsync(string templateId)
    {
        await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = $"templates/{templateId}/unzip"
            }
        );
    }

    /// <summary>
    /// Creates both PNG and PDF preview files for template visualization. Generates previews in parallel and returns URLs for both formats.
    /// </summary>
    public async Task<GenerateTemplatePreviewsResponse> GenerateTemplatePreviewsAsync(
        string templateId,
        GenerateTemplatePreviewsRequest request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = $"templates/{templateId}/render-previews",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GenerateTemplatePreviewsResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Generates a presigned URL for template access. Used for downloading template files from S3 storage.
    /// </summary>
    public async Task<GeneratePresignedGetUrlResponse> GeneratePresignedGetUrlAsync(
        string templateId
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"templates/{templateId}/presigned-url"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GeneratePresignedGetUrlResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Retrieves the template index.html file from S3 storage. Used for rendering the template in the browser.
    /// </summary>
    public async Task<GetTemplateIndexHtmlResponse> GetTemplateIndexHtmlAsync(string templateId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"templates/{templateId}/index-html"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetTemplateIndexHtmlResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Uploads the template index.html file to S3 storage. Used for rendering the template in the browser.
    /// </summary>
    public async Task UploadTemplateIndexHtmlAsync(
        string templateId,
        UploadTemplateIndexHtmlRequest request
    )
    {
        await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = $"templates/{templateId}/index-html",
                Body = request
            }
        );
    }

    /// <summary>
    /// Creates a new template by duplicating an existing template's content and metadata. Includes copying preview files and template index.
    /// </summary>
    public async Task<CloneTemplateResponse> CloneTemplateAsync(string templateId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = $"templates/{templateId}/clone"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CloneTemplateResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
