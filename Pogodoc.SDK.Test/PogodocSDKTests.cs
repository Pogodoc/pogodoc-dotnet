﻿namespace Pogodoc.SDK.Tests;

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetEnv;
using Pogodoc.SDK;
using Pogodoc.SDK.Types;
using PogodocApi;
using Xunit;
using Xunit.Abstractions;

public class EnvFixture
{
    public string ApiToken { get; }
    public string BaseUrl { get; }

    public EnvFixture()
    {
        Env.TraversePath().Load();

        ApiToken =
            Environment.GetEnvironmentVariable("POGODOC_API_TOKEN")
            ?? throw new InvalidOperationException("POGODOC_API_TOKEN not set");
        BaseUrl =
            Environment.GetEnvironmentVariable("LAMBDA_BASE_URL")
            ?? throw new InvalidOperationException("LAMBDA_BASE_URL not set");
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
            "a43bd040-fc6f-4fce-a65b-913cf059c51c",
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
                TemplateId = "a43bd040-fc6f-4fce-a65b-913cf059c51c",
                Type = InitializeRenderJobRequestType.Html,
                Target = InitializeRenderJobRequestTarget.Pdf,
                Data = sampleData,
                FormatOpts = new InitializeRenderJobRequestFormatOpts { FromPage = 1, ToPage = 2 },
            },
            ShouldWaitForRenderCompletion = true,
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
            ShouldWaitForRenderCompletion = true,
        };

        var result = await client.GenerateDocumentAsync(props);
        _output.WriteLine("STRING TEMPLATE RESULT: " + result.Output.Data.Url);
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
