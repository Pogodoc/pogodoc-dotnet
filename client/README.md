# Pogodoc C# Library

[![fern shield](https://img.shields.io/badge/%F0%9F%8C%BF-Built%20with%20Fern-brightgreen)](https://buildwithfern.com?utm_source=github&utm_medium=github&utm_campaign=readme&utm_source=Pogodoc%2FC%23)
[![nuget shield](https://img.shields.io/nuget/v/Pogodoc)](https://nuget.org/packages/Pogodoc)

The Pogodoc C# library provides convenient access to the Pogodoc API from C#.

## Requirements

This SDK requires:

## Installation

```sh
dotnet add package Pogodoc
```

## Usage

Instantiate and use the client with the following:

```csharp
using Pogodoc;

var client = new PogodocApiClient("TOKEN");
await client.Templates.SaveCreatedTemplateAsync(
    "templateId",
    new SaveCreatedTemplateRequest
    {
        TemplateInfo = new SaveCreatedTemplateRequestTemplateInfo
        {
            Title = "title",
            Description = "description",
            Type = SaveCreatedTemplateRequestTemplateInfoType.Docx,
            SampleData = new Dictionary<string, object>() { { "key", "value" } },
            Categories = new List<SaveCreatedTemplateRequestTemplateInfoCategoriesItem>()
            {
                SaveCreatedTemplateRequestTemplateInfoCategoriesItem.Invoice,
            },
        },
        PreviewIds = new SaveCreatedTemplateRequestPreviewIds
        {
            PngJobId = "pngJobId",
            PdfJobId = "pdfJobId",
        },
    }
);
```

## Exception Handling

When the API returns a non-success status code (4xx or 5xx response), a subclass of the following error
will be thrown.

```csharp
using Pogodoc;

try {
    var response = await client.Templates.SaveCreatedTemplateAsync(...);
} catch (PogodocApiApiException e) {
    System.Console.WriteLine(e.Body);
    System.Console.WriteLine(e.StatusCode);
}
```

## Advanced

### Retries

The SDK is instrumented with automatic retries with exponential backoff. A request will be retried as long
as the request is deemed retryable and the number of retry attempts has not grown larger than the configured
retry limit (default: 2).

A request is deemed retryable when any of the following HTTP status codes is returned:

- [408](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/408) (Timeout)
- [429](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/429) (Too Many Requests)
- [5XX](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500) (Internal Server Errors)

Use the `MaxRetries` request option to configure this behavior.

```csharp
var response = await client.Templates.SaveCreatedTemplateAsync(
    ...,
    new RequestOptions {
        MaxRetries: 0 // Override MaxRetries at the request level
    }
);
```

### Timeouts

The SDK defaults to a 30 second timeout. Use the `Timeout` option to configure this behavior.

```csharp
var response = await client.Templates.SaveCreatedTemplateAsync(
    ...,
    new RequestOptions {
        Timeout: TimeSpan.FromSeconds(3) // Override timeout to 3s
    }
);
```

## Contributing

While we value open-source contributions to this SDK, this library is generated programmatically.
Additions made directly to this library would have to be moved over to our generation code,
otherwise they would be overwritten upon the next generated release. Feel free to open a PR as
a proof of concept, but know that we will not be able to merge it as-is. We suggest opening
an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!