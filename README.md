## Pogodoc .NET SDK

The Pogodoc .NET SDK enables developers to seamlessly generate documents and manage templates using Pogodoc’s API.

### Installation

```bash
$ dotnet add package Pogodoc.SDK
```

### Setup

To use the SDK you will need an API key which can be obtained from the [Pogodoc Dashboard](https://pogodoc.com)

### Example

```cs
using System;
using System.Text.Json;
using DotNetEnv;
using Pogodoc.SDK;
using Pogodoc.SDK.Types;
using PogodocApi;

class Program
{
    private static Dictionary<string, object?>? ReadJsonFile(string filePath)
    {
        try
        {
            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, object?>>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error reading or parsing the JSON file at {filePath}: {ex.Message}"
            );
            return null;
        }
    }

    static void Main(string[] args)
    {
        Env.Load();

        var client = new PogodocSDK();

        var sampleData = ReadJsonFile("../../data/json_data/react.json");
        var templatePath = "../../data/templates/React-Demo-App.zip";

        var templateId = await client.SaveTemplateAsync(
            templatePath,
            new SaveCreatedTemplateRequestTemplateInfo
            {
                Title = "Invoice",
                Description = "Invoice description",
                Type = SaveCreatedTemplateRequestTemplateInfoType.React,
                SampleData = sampleData,
                Categories = [SaveCreatedTemplateRequestTemplateInfoCategoriesItem.Invoice],
            }
        );
        Console.WriteLine("Created template id: " + templateId);

        await client.UpdateTemplateAsync(
            templatePath,
            templateId,
            new UpdateTemplateRequestTemplateInfo
            {
                Title = "Invoice Updated",
                Description = "Description updated",
                Type = UpdateTemplateRequestTemplateInfoType.React,
                SampleData = sampleData,
                Categories = [SaveCreatedTemplateRequestTemplateInfoCategoriesItem.Invoice],
            }
        );
        Console.WriteLine("Template updated successfully");

        var response = await client.GenerateDocumentAsync(
            new GenerateDocumentProps
            {
                RenderConfig = new InitializeRenderJobRequest
                {
                    TemplateId = templateId,
                    Type = InitializeRenderJobRequestType.React,
                    Target = InitializeRenderJobRequestTarget.Pdf,
                    Data = sampleData,
                },
                ShouldWaitForRenderCompletion = true,
            }
        );

        Console.WriteLine($"Generated document url:\n {response?.Output?.Data.Url}");
    }
}

```

### License

MIT License
