using System.Net.Http;
using System.Text.Json;
using System.Threading;
using PogodocApi.Core;

namespace PogodocApi;

public partial class DocumentsClient
{
    private RawClient _client;

    internal DocumentsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Creates a new render job with a unique ID, sets up S3 storage for template and data files, and generates presigned upload URLs if needed. Requires subscription check.
    /// </summary>
    /// <example><code>
    /// await client.Documents.InitializeRenderJobAsync(
    ///     new InitializeRenderJobRequest
    ///     {
    ///         Type = InitializeRenderJobRequestType.Docx,
    ///         Target = InitializeRenderJobRequestTarget.Pdf,
    ///     }
    /// );
    /// </code></example>
    public async Task<InitializeRenderJobResponse> InitializeRenderJobAsync(
        InitializeRenderJobRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "documents/init",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<InitializeRenderJobResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PogodocApiException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            throw new PogodocApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Takes a previously initialized job, updates its status to in-progress, and triggers the rendering process using Puppeteer. Can optionally wait for render completion.
    /// </summary>
    /// <example><code>
    /// await client.Documents.StartRenderJobAsync("jobId", new StartRenderJobRequest());
    /// </code></example>
    public async Task<StartRenderJobResponse> StartRenderJobAsync(
        string jobId,
        StartRenderJobRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = string.Format(
                        "documents/{0}/render",
                        ValueConvert.ToPathParameterString(jobId)
                    ),
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<StartRenderJobResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PogodocApiException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            throw new PogodocApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Combines initialization and rendering in one step. Creates a job, uploads template/data directly, starts rendering, and adds the document to Strapi. Requires subscription check.
    /// </summary>
    /// <example><code>
    /// await client.Documents.StartImmediateRenderAsync(
    ///     new StartImmediateRenderRequest
    ///     {
    ///         StartImmediateRenderRequestData = new Dictionary&lt;string, object&gt;() { { "key", "value" } },
    ///         Type = StartImmediateRenderRequestType.Docx,
    ///         Target = StartImmediateRenderRequestTarget.Pdf,
    ///     }
    /// );
    /// </code></example>
    public async Task<StartImmediateRenderResponse> StartImmediateRenderAsync(
        StartImmediateRenderRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "documents/immediate-render",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<StartImmediateRenderResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PogodocApiException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            throw new PogodocApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Fetches detailed job information from S3 storage including job status, template ID, target format, and output details if available.
    /// </summary>
    /// <example><code>
    /// await client.Documents.GetJobStatusAsync("jobId");
    /// </code></example>
    public async Task<GetJobStatusResponse> GetJobStatusAsync(
        string jobId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = string.Format("jobs/{0}", ValueConvert.ToPathParameterString(jobId)),
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<GetJobStatusResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PogodocApiException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            throw new PogodocApiApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
