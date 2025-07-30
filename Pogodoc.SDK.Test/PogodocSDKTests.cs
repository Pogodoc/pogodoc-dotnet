namespace Pogodoc.Tests;

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetEnv;
using Pogodoc;
using Pogodoc.Types;
using Xunit;
using Xunit.Abstractions;

public class EnvFixture
{
    public string ApiToken { get; }
    public string BaseUrl { get; }

    public string TemplateId { get; }

    public EnvFixture()
    {
        Env.TraversePath().Load();

        ApiToken =
            Environment.GetEnvironmentVariable("POGODOC_API_TOKEN")
            ?? throw new InvalidOperationException("POGODOC_API_TOKEN not set");
        BaseUrl =
            Environment.GetEnvironmentVariable("POGODOC_BASE_URL")
            ?? throw new InvalidOperationException("POGODOC_BASE_URL not set");
        TemplateId =
            Environment.GetEnvironmentVariable("TEMPLATE_ID")
            ?? throw new InvalidOperationException("TEMPLATE_ID not set");
    }
}

public class PogodocClientTests : IClassFixture<EnvFixture>
{
    private readonly ITestOutputHelper _output;
    private readonly EnvFixture _env;

    public PogodocClientTests(ITestOutputHelper output, EnvFixture env)
    {
        _output = output;
        _env = env;
    }

    [Fact]
    public async Task CreateTemplateTest()
    {
        var client = new PogodocSDK(_env.ApiToken, _env.BaseUrl);

        var sampleData = ReadJsonFile("../../../../../../data/json_data/react.json");

        var templateId = await client.SaveTemplateAsync(
            "../../../../../../data/templates/React-Demo-App.zip",
            new SaveCreatedTemplateRequestTemplateInfo
            {
                Title = "Invoice-csharp",
                Description = "Invoice-csharp",
                Type = SaveCreatedTemplateRequestTemplateInfoType.Html,
                SampleData = sampleData,
            }
        );
        _output.WriteLine("templateId: " + templateId);
    }

    [Fact]
    public async Task UpdateTemplateTest()
    {
        var client = new PogodocSDK(_env.ApiToken, _env.BaseUrl);

        var sampleData = ReadJsonFile("../../../../../../data/json_data/react.json");

        var template = await client.UpdateTemplateAsync(
            "../../../../../../data/templates/React-Demo-App.zip",
            "33be1434-8901-412e-8c35-f40912ca4b64",
            new UpdateTemplateRequestTemplateInfo
            {
                Title = "Invoice-csharp-updated",
                Description = "Invoice-csharp-updated",
                Type = UpdateTemplateRequestTemplateInfoType.Html,
                SampleData = sampleData,
            }
        );
        _output.WriteLine("template: " + template);
    }

    [Fact]
    public async Task GenerateDocumentTest()
    {
        var client = new PogodocSDK(_env.ApiToken, _env.BaseUrl);

        var sampleData = ReadJsonFile("../../../../../../data/json_data/react.json");
        var props = new GenerateDocumentProps
        {
            RenderConfig = new InitializeRenderJobRequest
            {
                TemplateId = "33be1434-8901-412e-8c35-f40912ca4b64",
                Type = InitializeRenderJobRequestType.Html,
                Target = InitializeRenderJobRequestTarget.Pdf,
                Data = sampleData,
                FormatOpts = new InitializeRenderJobRequestFormatOpts { FromPage = 1, ToPage = 2 },
            },
        };

        var result = await client.GenerateDocumentAsync(props);
        _output.WriteLine("result: " + result.Output.Data.Url);
    }

    [Fact]
    public async Task GenerateDocumentFromStringTemplateTest()
    {
        var client = new PogodocSDK(_env.ApiToken, _env.BaseUrl);

        var props = new GenerateDocumentProps
        {
            RenderConfig = new InitializeRenderJobRequest
            {
                Type = InitializeRenderJobRequestType.Html,
                Target = InitializeRenderJobRequestTarget.Pdf,
                Data = new Dictionary<string, object> { { "name", "Ferdzo" } },
            },
            Template = "<h1>Hello <%= name %></h1>",
        };

        var result = await client.GenerateDocumentAsync(props);
        _output.WriteLine("STRING TEMPLATE RESULT: " + result.Output.Data.Url);
    }

    [Fact]
    public async Task DocumentGenerationsTest()
    {
        var client = new PogodocSDK(_env.ApiToken, _env.BaseUrl);

        var sampleData = new Dictionary<string, object> { { "name", "John Doe" } };

        var generateDocumentProps = new GenerateDocumentProps
        {
            RenderConfig = new InitializeRenderJobRequest
            {
                TemplateId = _env.TemplateId,
                Type = InitializeRenderJobRequestType.Html,
                Target = InitializeRenderJobRequestTarget.Pdf,
                Data = sampleData,
            },
        };

        var immediateResult = await client.GenerateDocumentImmediateAsync(
            new ImmediateGenerateDocumentProps
            {
                RenderConfig = new StartImmediateRenderRequest
                {
                    TemplateId = _env.TemplateId,
                    Type = StartImmediateRenderRequestType.Html,
                    Target = StartImmediateRenderRequestTarget.Pdf,
                    Data = sampleData,
                },
            }
        );
        _output.WriteLine("IMMEDIATE RESULT: " + immediateResult.Url);

        var jobId = await client.StartGenerateDocumentAsync(generateDocumentProps);
        _output.WriteLine("START RESULT: " + jobId);

        var pollResult = await client.PollForJobCompletionAsync(jobId);
        _output.WriteLine("POLL RESULT: " + pollResult.Status);

        var generateResult = await client.GenerateDocumentAsync(generateDocumentProps);
        _output.WriteLine("GENERATE RESULT: " + generateResult.Output.Data.Url);
    }

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
}
