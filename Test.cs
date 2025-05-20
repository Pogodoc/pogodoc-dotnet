// OLD TEST

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Pogodoc.Client.Types; // Assuming this is the namespace for your types
using PogodocApi; // Assuming this is the namespace for GetJobStatusResponse and other SDK responses

public class Test
{
    private static PogodocClient? client;
    private static object? sampleData;
    private static string templatePath = "../../data/templates/React-Demo-App.zip"; // Adjust path as needed

    public static async Task Main(string[] args)
    {
        // Load environment variables (ensure these are set in your environment)
        // For local development, you might use a .env file and a library to load it,
        // or set them in your launchSettings.json for Rider/Visual Studio.
        var apiToken = Environment.GetEnvironmentVariable("POGODOC_API_TOKEN");
        var baseUrl = Environment.GetEnvironmentVariable("LAMBDA_BASE_URL");

        if (string.IsNullOrEmpty(apiToken))
        {
            Console.WriteLine("POGODOC_API_TOKEN environment variable not set.");
            return;
        }

        client = new PogodocClient(apiToken, baseUrl);
        sampleData = ReadJsonFile("../../data/json_data/react.json"); // Adjust path as needed

        if (sampleData == null)
        {
            Console.WriteLine("Failed to read sample data.");
            return;
        }

        try
        {
            // Test SaveTemplateAsync
            Console.WriteLine("Testing SaveTemplateAsync...");
            var templateId = await client.SaveTemplateAsync(
                templatePath,
                new SaveTemplateMetadata
                {
                    Path = templatePath, // Path is part of method signature, not metadata object in C#
                    Title = "Invoice",
                    Description = "Invoice template",
                    Type = "html",
                    Categories = new[] { "invoice" },
                    SampleData = sampleData,
                }
            );
            Console.WriteLine($"SaveTemplateAsync successful. Template ID: {templateId}");

            if (string.IsNullOrEmpty(templateId))
            {
                Console.WriteLine("Template ID is required for further tests.");
                return;
            }

            // Test GenerateDocumentAsync
            Console.WriteLine("\nTesting GenerateDocumentAsync...");
            var documentOutput = await client.GenerateDocumentAsync(
                new GenerateDocumentProps
                {
                    TemplateId = templateId,
                    Data = sampleData,
                    RenderConfig = new RenderConfig
                    {
                        Type = "html",
                        Target = "pdf",
                        FormatOpts = new { FromPage = 1, ToPage = 1 }, // Example format options
                    },
                    ShouldWaitForRenderCompletion = true,
                }
            );
            Console.WriteLine(
                $"GenerateDocumentAsync successful. Document Output Job ID: {documentOutput.JobId}"
            ); // Assuming GetJobStatusResponse has JobId

            // Test UpdateTemplateAsync
            // Note: The TypeScript test uses a hardcoded templateId for update.
            // You might want to use the templateId obtained from SaveTemplateAsync or a specific test templateId.
            Console.WriteLine("\nTesting UpdateTemplateAsync...");
            var updatedTemplateId = await client.UpdateTemplateAsync(
                templatePath,
                new UpdateTemplateProps
                {
                    Path = templatePath, // Path is part of method signature, not metadata object in C#
                    TemplateId = "9c4ace0a-8d6f-4371-b455-38508f1f8ac2", // Replace with a valid template ID for testing updates
                    Title = "Invoice (Updated)",
                    Description = "Invoice template (Updated)",
                    Type = "html",
                    Categories = new[] { "invoice" },
                    SampleData = sampleData,
                }
            );
            Console.WriteLine(
                $"UpdateTemplateAsync successful. Updated Content ID: {updatedTemplateId}"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    private static object? ReadJsonFile(string filePath)
    {
        try
        {
            var jsonString = File.ReadAllText(filePath);
            // Deserialize to a generic object or a specific type if known
            return JsonSerializer.Deserialize<object>(jsonString);
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
