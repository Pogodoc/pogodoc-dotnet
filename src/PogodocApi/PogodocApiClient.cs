using PogodocApi;
using PogodocApi.Core;

#nullable enable

namespace PogodocApi;

public partial class PogodocApiClient
{
    private RawClient _client;

    public PogodocApiClient(string? token = null, ClientOptions? clientOptions = null)
    {
        _client = new RawClient(
            new Dictionary<string, string>()
            {
                { "Authorization", $"Bearer {token}" },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "PogodocApi" },
                { "X-Fern-SDK-Version", "0.0.23" },
            },
            clientOptions ?? new ClientOptions()
        );
        Templates = new TemplatesClient(_client);
        Documents = new DocumentsClient(_client);
        Tokens = new TokensClient(_client);
    }

    public TemplatesClient Templates { get; init; }

    public DocumentsClient Documents { get; init; }

    public TokensClient Tokens { get; init; }
}
