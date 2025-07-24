## Pogodoc .NET SDK

The Pogodoc .NET SDK enables developers to seamlessly generate documents and manage templates using Pogodoc’s API.

### Installation

```bash
$ dotnet add package Pogodoc.SDK
```

### Setup

To use the SDK you will need an API key which can be obtained from the [Pogodoc Dashboard](https://app.pogodoc.com)

### Example

```cs
using Pogodoc;
using Pogodoc.Types;

class Program
{
    static void Main(string[] args)
    {
        var client = new PogodocSDK("YOUR_POGODOC_API_TOKEN");

        var response = await client.GenerateDocumentAsync(
            new GenerateDocumentProps
            {
                RenderConfig = new InitializeRenderJobRequest
                {
                    TemplateId = "your-template-id",
                    Type = InitializeRenderJobRequestType.React,
                    Target = InitializeRenderJobRequestTarget.Pdf,
                    Data = new Dictionary<string, object> { { "name", "John Doe" } },
                },
            }
        );

        Console.WriteLine($"Generated document url:\n {response?.Output?.Data.Url}");
    }
}

```

### License

MIT License
