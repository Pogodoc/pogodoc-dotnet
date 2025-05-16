using System.Net.Http;
using System.Threading;
using global::System.Threading.Tasks;
using PogodocApi.Core;

namespace PogodocApi;

public partial class TokensClient
{
    private RawClient _client;

    internal TokensClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Invalidates an API token by storing it in the deleted tokens S3 bucket, preventing future use of the token for authentication.
    /// </summary>
    /// <example><code>
    /// await client.Tokens.DeleteApiTokenAsync("tokenId");
    /// </code></example>
    public async global::System.Threading.Tasks.Task DeleteApiTokenAsync(
        string tokenId,
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
                        "api-tokens/{0}",
                        ValueConvert.ToPathParameterString(tokenId)
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
}
