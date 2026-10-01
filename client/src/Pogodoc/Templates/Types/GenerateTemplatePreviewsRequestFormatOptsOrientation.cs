using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<GenerateTemplatePreviewsRequestFormatOptsOrientation>))]
[Serializable]
public readonly record struct GenerateTemplatePreviewsRequestFormatOptsOrientation : IStringEnum
{
    public static readonly GenerateTemplatePreviewsRequestFormatOptsOrientation Landscape = new(
        Values.Landscape
    );

    public static readonly GenerateTemplatePreviewsRequestFormatOptsOrientation Portrait = new(
        Values.Portrait
    );

    public GenerateTemplatePreviewsRequestFormatOptsOrientation(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static GenerateTemplatePreviewsRequestFormatOptsOrientation FromCustom(string value)
    {
        return new GenerateTemplatePreviewsRequestFormatOptsOrientation(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(
        GenerateTemplatePreviewsRequestFormatOptsOrientation value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        GenerateTemplatePreviewsRequestFormatOptsOrientation value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        GenerateTemplatePreviewsRequestFormatOptsOrientation value
    ) => value.Value;

    public static explicit operator GenerateTemplatePreviewsRequestFormatOptsOrientation(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Landscape = "landscape";

        public const string Portrait = "portrait";
    }
}
