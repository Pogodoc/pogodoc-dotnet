namespace Pogodoc.Client.Tests;
using Xunit
using Pogodoc.Client;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task Test1()
    {
        var apiToken = Environment.GetEnvironmentVariable("POGODOC_API_TOKEN");
        var baseUrl = Environment.GetEnvironmentVariable("LAMBDA_BASE_URL");

        Assert.NotNull(apiToken);
        Assert.NotNull(baseUrl);
        _output.WriteLine("apiToken: " + apiToken);
        _output.WriteLine("baseUrl: " + baseUrl);
        var client = new PogodocClient(apiToken, baseUrl);

        var template = await client.Templates.InitializeTemplateCreationAsync();
        _output.WriteLine("template: " + template);
    }
}
