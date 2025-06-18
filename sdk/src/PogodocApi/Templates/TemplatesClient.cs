using System.Net.Http;
using System.Text.Json;
using System.Threading;
using global::System.Threading.Tasks;
using PogodocApi.Core;

namespace PogodocApi;

public partial class TemplatesClient
{
    private RawClient _client;

    internal TemplatesClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Initializes template creation by generating a unique ID and providing a presigned URL for template ZIP upload. Sets unfinished tag for tracking incomplete templates.
    /// </summary>
    /// <example><code>
    /// await client.Templates.InitializeTemplateCreationAsync();
    /// </code></example>
    public async Task<InitializeTemplateCreationResponse> InitializeTemplateCreationAsync(
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
                    Path = "templates/init",
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
                return JsonUtils.Deserialize<InitializeTemplateCreationResponse>(responseBody)!;
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
    /// Finalizes template creation by saving template info to Strapi, copying preview files to permanent storage, and creating template index. Removes unfinished tag upon completion.
    /// </summary>
    /// <example><code>
    /// await client.Templates.SaveCreatedTemplateAsync(
    ///     "templateId",
    ///     new SaveCreatedTemplateRequest
    ///     {
    ///         TemplateInfo = new SaveCreatedTemplateRequestTemplateInfo
    ///         {
    ///             Title = "title",
    ///             Description = "description",
    ///             Type = SaveCreatedTemplateRequestTemplateInfoType.Docx,
    ///             SampleData = new Dictionary&lt;string, object&gt;() { { "key", "value" } },
    ///             Categories = new List&lt;SaveCreatedTemplateRequestTemplateInfoCategoriesItem&gt;()
    ///             {
    ///                 SaveCreatedTemplateRequestTemplateInfoCategoriesItem.Invoice,
    ///             },
    ///         },
    ///         PreviewIds = new SaveCreatedTemplateRequestPreviewIds
    ///         {
    ///             PngJobId = "pngJobId",
    ///             PdfJobId = "pdfJobId",
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task SaveCreatedTemplateAsync(
        string templateId,
        SaveCreatedTemplateRequest request,
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
                        "templates/{0}",
                        ValueConvert.ToPathParameterString(templateId)
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
            return;
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
    /// Updates template content, handles S3 storage cleanup for old content, updates template metadata in Strapi, and manages preview files. Removes unfinished tags after successful update.
    /// </summary>
    /// <example><code>
    /// await client.Templates.UpdateTemplateAsync(
    ///     "templateId",
    ///     new UpdateTemplateRequest
    ///     {
    ///         TemplateInfo = new UpdateTemplateRequestTemplateInfo
    ///         {
    ///             Title = "title",
    ///             Description = "description",
    ///             Type = UpdateTemplateRequestTemplateInfoType.Docx,
    ///             SampleData = new Dictionary&lt;string, object&gt;() { { "key", "value" } },
    ///             Categories = new List&lt;UpdateTemplateRequestTemplateInfoCategoriesItem&gt;()
    ///             {
    ///                 UpdateTemplateRequestTemplateInfoCategoriesItem.Invoice,
    ///             },
    ///         },
    ///         PreviewIds = new UpdateTemplateRequestPreviewIds
    ///         {
    ///             PngJobId = "pngJobId",
    ///             PdfJobId = "pdfJobId",
    ///         },
    ///         ContentId = "contentId",
    ///     }
    /// );
    /// </code></example>
    public async Task<UpdateTemplateResponse> UpdateTemplateAsync(
        string templateId,
        UpdateTemplateRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Put,
                    Path = string.Format(
                        "templates/{0}",
                        ValueConvert.ToPathParameterString(templateId)
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
                return JsonUtils.Deserialize<UpdateTemplateResponse>(responseBody)!;
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
    /// Deletes a template from Strapi and associated S3 storage. Removes all associated files and metadata.
    /// </summary>
    /// <example><code>
    /// await client.Templates.DeleteTemplateAsync("templateId");
    /// </code></example>
    public async Task<DeleteTemplateResponse> DeleteTemplateAsync(
        string templateId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Delete,
                    Path = string.Format(
                        "templates/{0}",
                        ValueConvert.ToPathParameterString(templateId)
                    ),
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
                return JsonUtils.Deserialize<DeleteTemplateResponse>(responseBody)!;
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
    /// Extracts contents from an uploaded template ZIP file and stores individual files in the appropriate S3 storage structure.
    /// </summary>
    /// <example><code>
    /// await client.Templates.ExtractTemplateFilesAsync("templateId");
    /// </code></example>
    public async global::System.Threading.Tasks.Task ExtractTemplateFilesAsync(
        string templateId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethodExtensions.Patch,
                    Path = string.Format(
                        "templates/{0}/unzip",
                        ValueConvert.ToPathParameterString(templateId)
                    ),
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
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
    /// Creates both PNG and PDF preview files for template visualization. Generates previews in parallel and returns URLs for both formats.
    /// </summary>
    /// <example><code>
    /// await client.Templates.GenerateTemplatePreviewsAsync(
    ///     "templateId",
    ///     new GenerateTemplatePreviewsRequest
    ///     {
    ///         Type = GenerateTemplatePreviewsRequestType.Docx,
    ///         Data = new Dictionary&lt;string, object&gt;() { { "key", "value" } },
    ///     }
    /// );
    /// </code></example>
    public async Task<GenerateTemplatePreviewsResponse> GenerateTemplatePreviewsAsync(
        string templateId,
        GenerateTemplatePreviewsRequest request,
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
                        "templates/{0}/render-previews",
                        ValueConvert.ToPathParameterString(templateId)
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
                return JsonUtils.Deserialize<GenerateTemplatePreviewsResponse>(responseBody)!;
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
    /// Generates a presigned URL for template access. Used for downloading template files from S3 storage.
    /// </summary>
    /// <example><code>
    /// await client.Templates.GeneratePresignedGetUrlAsync("templateId");
    /// </code></example>
    public async Task<GeneratePresignedGetUrlResponse> GeneratePresignedGetUrlAsync(
        string templateId,
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
                    Path = string.Format(
                        "templates/{0}/presigned-url",
                        ValueConvert.ToPathParameterString(templateId)
                    ),
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
                return JsonUtils.Deserialize<GeneratePresignedGetUrlResponse>(responseBody)!;
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
    /// Retrieves the template index.html file from S3 storage. Used for rendering the template in the browser.
    /// </summary>
    /// <example><code>
    /// await client.Templates.GetTemplateIndexHtmlAsync("templateId");
    /// </code></example>
    public async Task<GetTemplateIndexHtmlResponse> GetTemplateIndexHtmlAsync(
        string templateId,
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
                    Path = string.Format(
                        "templates/{0}/index-html",
                        ValueConvert.ToPathParameterString(templateId)
                    ),
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
                return JsonUtils.Deserialize<GetTemplateIndexHtmlResponse>(responseBody)!;
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
    /// Uploads the template index.html file to S3 storage. Used for rendering the template in the browser.
    /// </summary>
    /// <example><code>
    /// await client.Templates.UploadTemplateIndexHtmlAsync(
    ///     "templateId",
    ///     new UploadTemplateIndexHtmlRequest { TemplateIndex = "templateIndex" }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task UploadTemplateIndexHtmlAsync(
        string templateId,
        UploadTemplateIndexHtmlRequest request,
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
                        "templates/{0}/index-html",
                        ValueConvert.ToPathParameterString(templateId)
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
            return;
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
    /// Creates a new template by duplicating an existing template's content and metadata. Includes copying preview files and template index.
    /// </summary>
    /// <example><code>
    /// await client.Templates.CloneTemplateAsync("templateId");
    /// </code></example>
    public async Task<CloneTemplateResponse> CloneTemplateAsync(
        string templateId,
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
                        "templates/{0}/clone",
                        ValueConvert.ToPathParameterString(templateId)
                    ),
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
                return JsonUtils.Deserialize<CloneTemplateResponse>(responseBody)!;
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
