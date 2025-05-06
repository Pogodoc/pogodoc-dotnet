using System.Net.Http;
using System.Text.Json;
using PogodocApi;
using PogodocApi.Core;

#nullable enable

namespace PogodocApi;

public class DocumentsClient
{
    private RawClient _client;

    public DocumentsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Creates a new render job with a unique ID, sets up S3 storage for template and data files, and generates presigned upload URLs if needed. Requires subscription check.
    /// </summary>
    public async Task<InitializeRenderJobResponse> InitializeRenderJobAsync(
        InitializeRenderJobRequest request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "documents/init",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<InitializeRenderJobResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Takes a previously initialized job, updates its status to in-progress, and triggers the rendering process using Puppeteer. Can optionally wait for render completion.
    /// </summary>
    public async Task<StartRenderJobResponse> StartRenderJobAsync(
        string jobId,
        StartRenderJobRequest request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = $"documents/{jobId}/render",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<StartRenderJobResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Generates a preview by creating a single-page render job, processing it immediately, and returning the output URL. Used for template visualization.
    /// </summary>
    public async Task<GenerateDocumentPreviewResponse> GenerateDocumentPreviewAsync(
        GenerateDocumentPreviewRequest request
    )
    {
        var _query = new Dictionary<string, object>() { { "templateId", request.TemplateId }, };
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "documents/render-preview",
                Query = _query
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GenerateDocumentPreviewResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Combines initialization and rendering in one step. Creates a job, uploads template/data directly, starts rendering, and adds the document to Strapi. Requires subscription check.
    /// </summary>
    public async Task<StartImmediateRenderResponse> StartImmediateRenderAsync(
        StartImmediateRenderRequest request
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "documents/immediate-render",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<StartImmediateRenderResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Fetches detailed job information from S3 storage including job status, template ID, target format, and output details if available.
    /// </summary>
    public async Task<GetJobStatusResponse> GetJobStatusAsync(string jobId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = $"jobs/{jobId}" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<GetJobStatusResponse>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
