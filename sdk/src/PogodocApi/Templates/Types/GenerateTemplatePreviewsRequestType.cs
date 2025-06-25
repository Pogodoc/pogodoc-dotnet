using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<GenerateTemplatePreviewsRequestType>))]
[Serializable]
public readonly record struct GenerateTemplatePreviewsRequestType : IStringEnum
{
    public static readonly GenerateTemplatePreviewsRequestType Docx = new(Values.Docx);

    public static readonly GenerateTemplatePreviewsRequestType Xlsx = new(Values.Xlsx);

    public static readonly GenerateTemplatePreviewsRequestType Pptx = new(Values.Pptx);

    public static readonly GenerateTemplatePreviewsRequestType Ejs = new(Values.Ejs);

    public static readonly GenerateTemplatePreviewsRequestType Html = new(Values.Html);

    public static readonly GenerateTemplatePreviewsRequestType Latex = new(Values.Latex);

    public static readonly GenerateTemplatePreviewsRequestType React = new(Values.React);

    public GenerateTemplatePreviewsRequestType(string value)
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
    public static GenerateTemplatePreviewsRequestType FromCustom(string value)
    {
        return new GenerateTemplatePreviewsRequestType(value);
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

    public static bool operator ==(GenerateTemplatePreviewsRequestType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GenerateTemplatePreviewsRequestType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GenerateTemplatePreviewsRequestType value) =>
        value.Value;

    public static explicit operator GenerateTemplatePreviewsRequestType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Docx = "docx";

        public const string Xlsx = "xlsx";

        public const string Pptx = "pptx";

        public const string Ejs = "ejs";

        public const string Html = "html";

        public const string Latex = "latex";

        public const string React = "react";
    }
}
