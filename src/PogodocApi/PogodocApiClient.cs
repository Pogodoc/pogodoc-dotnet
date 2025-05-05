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
                { "X-Fern-SDK-Version", "0.0.12" },
            },
            clientOptions ?? new ClientOptions()
        );
        Templates = new TemplatesClient(_client);
        Render = new RenderClient(_client);
        Tokens = new TokensClient(_client);
    }

    public TemplatesClient Templates { get; init; }

    public RenderClient Render { get; init; }

    public TokensClient Tokens { get; init; }
}
