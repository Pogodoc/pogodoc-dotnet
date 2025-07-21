using global::System.Net.Http;
using global::System.Net.Http.Headers;
using global::System.Text;
using SystemTask = global::System.Threading.Tasks.Task;

namespace Pogodoc.Core;

/// <summary>
/// Utility class for making raw HTTP requests to the API.
/// </summary>
internal partial class RawClient(ClientOptions clientOptions)
{
    private const int MaxRetryDelayMs = 60000;
    internal int BaseRetryDelay { get; set; } = 1000;

    /// <summary>
    /// The client options applied on every request.
    /// </summary>
    internal readonly ClientOptions Options = clientOptions;

    [Obsolete("Use SendRequestAsync instead.")]
    internal Task<Pogodoc.Core.ApiResponse> MakeRequestAsync(
        Pogodoc.Core.BaseRequest request,
        CancellationToken cancellationToken = default
    )
    {
        return SendRequestAsync(request, cancellationToken);
    }

    internal async Task<Pogodoc.Core.ApiResponse> SendRequestAsync(
        Pogodoc.Core.BaseRequest request,
        CancellationToken cancellationToken = default
    )
    {
        // Apply the request timeout.
        var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var timeout = request.Options?.Timeout ?? Options.Timeout;
        cts.CancelAfter(timeout);

        var httpRequest = CreateHttpRequest(request);
        // Send the request.
        return await SendWithRetriesAsync(httpRequest, request.Options, cts.Token)
            .ConfigureAwait(false);
    }

    internal async Task<Pogodoc.Core.ApiResponse> SendRequestAsync(
        HttpRequestMessage request,
        IRequestOptions? options,
        CancellationToken cancellationToken = default
    )
    {
        // Apply the request timeout.
        var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var timeout = options?.Timeout ?? Options.Timeout;
        cts.CancelAfter(timeout);

        // Send the request.
        return await SendWithRetriesAsync(request, options, cts.Token).ConfigureAwait(false);
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clonedRequest = new HttpRequestMessage(request.Method, request.RequestUri);
        clonedRequest.Version = request.Version;

        if (request.Content != null)
        {
            switch (request.Content)
            {
                case MultipartContent oldMultipartFormContent:
                    var originalBoundary =
                        oldMultipartFormContent
                            .Headers.ContentType?.Parameters.FirstOrDefault(p =>
                                p.Name.Equals("boundary", StringComparison.OrdinalIgnoreCase)
                            )
                            ?.Value?.Trim('"') ?? Guid.NewGuid().ToString();
                    var newMultipartContent = oldMultipartFormContent switch
                    {
                        MultipartFormDataContent => new MultipartFormDataContent(originalBoundary),
                        _ => new MultipartContent(),
                    };
                    foreach (var content in oldMultipartFormContent)
                    {
                        var ms = new MemoryStream();
                        await content.CopyToAsync(ms).ConfigureAwait(false);
                        ms.Position = 0;
                        var newPart = new StreamContent(ms);
                        foreach (var header in content.Headers)
                        {
                            newPart.Headers.TryAddWithoutValidation(header.Key, header.Value);
                        }
                        newMultipartContent.Add(newPart);
                    }
                    clonedRequest.Content = newMultipartContent;
                    break;

                case StringContent stringContent:
                    var stringValue = await stringContent.ReadAsStringAsync().ConfigureAwait(false);
                    var newStringContent = new StringContent(
                        stringValue,
                        Encoding.UTF8,
                        stringContent.Headers.ContentType?.MediaType ?? "text/plain"
                    );
                    CopyContentHeaders(stringContent, newStringContent);
                    clonedRequest.Content = newStringContent;
                    break;

                case FormUrlEncodedContent formContent:
                    var formString = await formContent.ReadAsStringAsync().ConfigureAwait(false);
                    var formData = System.Web.HttpUtility.ParseQueryString(formString);
                    var keyValuePairs = formData.AllKeys.Select(key => new KeyValuePair<
                        string,
                        string
                    >(key ?? string.Empty, formData[key] ?? string.Empty));
                    var newFormContent = new FormUrlEncodedContent(keyValuePairs);
                    CopyContentHeaders(formContent, newFormContent);
                    clonedRequest.Content = newFormContent;
                    break;

                case ByteArrayContent byteArrayContent:
                    var byteArray = await byteArrayContent
                        .ReadAsByteArrayAsync()
                        .ConfigureAwait(false);
                    var newByteArrayContent = new ByteArrayContent(byteArray);
                    CopyContentHeaders(byteArrayContent, newByteArrayContent);
                    clonedRequest.Content = newByteArrayContent;
                    break;

                default:
                    var contentBytes = await request
                        .Content.ReadAsByteArrayAsync()
                        .ConfigureAwait(false);
                    var newContent = new ByteArrayContent(contentBytes);
                    CopyContentHeaders(request.Content, newContent);
                    clonedRequest.Content = newContent;
                    break;
            }
        }

        foreach (var header in request.Headers)
        {
            clonedRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clonedRequest;
    }

    private static void CopyContentHeaders(HttpContent source, HttpContent destination)
    {
        foreach (var header in source.Headers)
        {
            destination.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
    }

    /// <summary>
    /// Sends the request with retries, unless the request content is not retryable,
    /// such as stream requests and multipart form data with stream content.
    /// </summary>
    private async Task<Pogodoc.Core.ApiResponse> SendWithRetriesAsync(
        HttpRequestMessage request,
        IRequestOptions? options,
        CancellationToken cancellationToken
    )
    {
        var httpClient = options?.HttpClient ?? Options.HttpClient;
        var maxRetries = options?.MaxRetries ?? Options.MaxRetries;
        var isRetryableContent = IsRetryableContent(request);

        // Clone the original request for the first attempt to avoid disposal issues
        using var firstRequest = await CloneRequestAsync(request).ConfigureAwait(false);
        var response = await httpClient.SendAsync(firstRequest, cancellationToken).ConfigureAwait(false);

        if (!isRetryableContent)
        {
            return new Pogodoc.Core.ApiResponse
            {
                StatusCode = (int)response.StatusCode,
                Raw = response,
            };
        }

        for (var i = 0; i < maxRetries; i++)
        {
            if (!ShouldRetry(response))
            {
                break;
            }

            var delayMs = Math.Min(BaseRetryDelay * (int)Math.Pow(2, i), MaxRetryDelayMs);
            await SystemTask.Delay(delayMs, cancellationToken).ConfigureAwait(false);
            
            // Dispose the previous response before creating a new one
            response.Dispose();
            
            using var retryRequest = await CloneRequestAsync(request).ConfigureAwait(false);
            response = await httpClient
                .SendAsync(retryRequest, cancellationToken)
                .ConfigureAwait(false);
        }

        return new Pogodoc.Core.ApiResponse
        {
            StatusCode = (int)response.StatusCode,
            Raw = response,
        };
    }

    private static bool ShouldRetry(HttpResponseMessage response)
    {
        var statusCode = (int)response.StatusCode;
        return statusCode is 408 or 429 or >= 500;
    }

    private static bool IsRetryableContent(HttpRequestMessage request)
    {
        return request.Content switch
        {
            IIsRetryableContent c => c.IsRetryable,
            StreamContent => false,
            MultipartContent content => !content.Any(c => c is StreamContent),
            _ => true,
        };
    }

    internal HttpRequestMessage CreateHttpRequest(Pogodoc.Core.BaseRequest request)
    {
        var url = BuildUrl(request);
        var httpRequest = new HttpRequestMessage(request.Method, url);
        httpRequest.Content = request.CreateContent();
        var mergedHeaders = new Dictionary<string, List<string>>();
        MergeHeaders(mergedHeaders, Options.Headers);
        MergeAdditionalHeaders(mergedHeaders, Options.AdditionalHeaders);
        MergeHeaders(mergedHeaders, request.Headers);
        MergeHeaders(mergedHeaders, request.Options?.Headers);

        MergeAdditionalHeaders(mergedHeaders, request.Options?.AdditionalHeaders ?? []);
        SetHeaders(httpRequest, mergedHeaders);
        return httpRequest;
    }

    private static string BuildUrl(Pogodoc.Core.BaseRequest request)
    {
        var baseUrl = request.Options?.BaseUrl ?? request.BaseUrl;
        var trimmedBaseUrl = baseUrl.TrimEnd('/');
        var trimmedBasePath = request.Path.TrimStart('/');
        var url = $"{trimmedBaseUrl}/{trimmedBasePath}";

        var queryParameters = GetQueryParameters(request);
        if (!queryParameters.Any())
            return url;

        url += "?";
        url = queryParameters.Aggregate(
            url,
            (current, queryItem) =>
            {
                if (
                    queryItem.Value
                    is global::System.Collections.IEnumerable collection
                        and not string
                )
                {
                    var items = collection
                        .Cast<object>()
                        .Select(value => $"{queryItem.Key}={value}")
                        .ToList();
                    if (items.Any())
                    {
                        current += string.Join("&", items) + "&";
                    }
                }
                else
                {
                    current += $"{queryItem.Key}={queryItem.Value}&";
                }

                return current;
            }
        );
        url = url[..^1];
        return url;
    }

    private static List<KeyValuePair<string, string>> GetQueryParameters(
        Pogodoc.Core.BaseRequest request
    )
    {
        var result = TransformToKeyValuePairs(request.Query);
        if (
            request.Options?.AdditionalQueryParameters is null
            || !request.Options.AdditionalQueryParameters.Any()
        )
        {
            return result;
        }

        var additionalKeys = request
            .Options.AdditionalQueryParameters.Select(p => p.Key)
            .Distinct();
        foreach (var key in additionalKeys)
        {
            result.RemoveAll(kv => kv.Key == key);
        }

        result.AddRange(request.Options.AdditionalQueryParameters);
        return result;
    }

    private static List<KeyValuePair<string, string>> TransformToKeyValuePairs(
        Dictionary<string, object> inputDict
    )
    {
        var result = new List<KeyValuePair<string, string>>();
        foreach (var kvp in inputDict)
        {
            switch (kvp.Value)
            {
                case string str:
                    result.Add(new KeyValuePair<string, string>(kvp.Key, str));
                    break;
                case IEnumerable<string> strList:
                {
                    foreach (var value in strList)
                    {
                        result.Add(new KeyValuePair<string, string>(kvp.Key, value));
                    }

                    break;
                }
            }
        }

        return result;
    }

    private static void MergeHeaders(
        Dictionary<string, List<string>> mergedHeaders,
        Headers? headers
    )
    {
        if (headers is null)
        {
            return;
        }

        foreach (var header in headers)
        {
            var value = header.Value?.Match(str => str, func => func.Invoke());
            if (value != null)
            {
                mergedHeaders[header.Key] = [value];
            }
        }
    }

    private static void MergeAdditionalHeaders(
        Dictionary<string, List<string>> mergedHeaders,
        IEnumerable<KeyValuePair<string, string?>>? headers
    )
    {
        if (headers is null)
        {
            return;
        }

        var usedKeys = new HashSet<string>();
        foreach (var header in headers)
        {
            if (header.Value is null)
            {
                mergedHeaders.Remove(header.Key);
                usedKeys.Remove(header.Key);
                continue;
            }

            if (usedKeys.Contains(header.Key))
            {
                mergedHeaders[header.Key].Add(header.Value);
            }
            else
            {
                mergedHeaders[header.Key] = [header.Value];
                usedKeys.Add(header.Key);
            }
        }
    }

    private void SetHeaders(
        HttpRequestMessage httpRequest,
        Dictionary<string, List<string>> mergedHeaders
    )
    {
        foreach (var kv in mergedHeaders)
        {
            foreach (var header in kv.Value)
            {
                if (header is null)
                {
                    continue;
                }

                httpRequest.Headers.TryAddWithoutValidation(kv.Key, header);
            }
        }
    }

    private static (Encoding encoding, string? charset, string mediaType) ParseContentTypeOrDefault(
        string? contentType,
        Encoding encodingFallback,
        string mediaTypeFallback
    )
    {
        var encoding = encodingFallback;
        var mediaType = mediaTypeFallback;
        string? charset = null;
        if (string.IsNullOrEmpty(contentType))
        {
            return (encoding, charset, mediaType);
        }

        if (!MediaTypeHeaderValue.TryParse(contentType, out var mediaTypeHeaderValue))
        {
            return (encoding, charset, mediaType);
        }

        if (!string.IsNullOrEmpty(mediaTypeHeaderValue.CharSet))
        {
            charset = mediaTypeHeaderValue.CharSet;
            encoding = Encoding.GetEncoding(mediaTypeHeaderValue.CharSet);
        }

        if (!string.IsNullOrEmpty(mediaTypeHeaderValue.MediaType))
        {
            mediaType = mediaTypeHeaderValue.MediaType;
        }

        return (encoding, charset, mediaType);
    }

    /// <inheritdoc />
    [Obsolete("Use Pogodoc.Core.ApiResponse instead.")]
    internal record ApiResponse : Pogodoc.Core.ApiResponse;

    /// <inheritdoc />
    [Obsolete("Use Pogodoc.Core.BaseRequest instead.")]
    internal abstract record BaseApiRequest : Pogodoc.Core.BaseRequest;

    /// <inheritdoc />
    [Obsolete("Use Pogodoc.Core.EmptyRequest instead.")]
    internal abstract record EmptyApiRequest : Pogodoc.Core.EmptyRequest;

    /// <inheritdoc />
    [Obsolete("Use Pogodoc.Core.JsonRequest instead.")]
    internal abstract record JsonApiRequest : Pogodoc.Core.JsonRequest;

    /// <inheritdoc />
    [Obsolete("Use Pogodoc.Core.MultipartFormRequest instead.")]
    internal abstract record MultipartFormRequest : Pogodoc.Core.MultipartFormRequest;

    /// <inheritdoc />
    [Obsolete("Use Pogodoc.Core.StreamRequest instead.")]
    internal abstract record StreamApiRequest : Pogodoc.Core.StreamRequest;
}
