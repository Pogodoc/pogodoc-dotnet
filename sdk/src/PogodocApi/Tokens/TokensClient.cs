using System.Net.Http;
using System.Text.Json;
using PogodocApi.Core;

#nullable enable

namespace PogodocApi;

public class TokensClient
{
    private RawClient _client;

    public TokensClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Invalidates an API token by storing it in the deleted tokens S3 bucket, preventing future use of the token for authentication.
    /// </summary>
    public async Task<object> DeleteApiTokenAsync(string tokenId)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"api-tokens/{tokenId}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<object>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
