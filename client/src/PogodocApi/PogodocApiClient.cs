using PogodocApi.Core;

namespace PogodocApi;

public partial class PogodocApiClient
{
    private readonly RawClient _client;

    public PogodocApiClient(string? token = null, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "Authorization", $"Bearer {token}" },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "PogodocApi" },
                { "X-Fern-SDK-Version", Version.Current },
            }
        );
        clientOptions ??= new ClientOptions();
        foreach (var header in defaultHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        _client = new RawClient(clientOptions);
        Templates = new TemplatesClient(_client);
        Documents = new DocumentsClient(_client);
    }

    public TemplatesClient Templates { get; }

    public DocumentsClient Documents { get; }
}
