using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<GetUserTemplatesRequestType>))]
[Serializable]
public readonly record struct GetUserTemplatesRequestType : IStringEnum
{
    public static readonly GetUserTemplatesRequestType Docx = new(Values.Docx);

    public static readonly GetUserTemplatesRequestType Xlsx = new(Values.Xlsx);

    public static readonly GetUserTemplatesRequestType Pptx = new(Values.Pptx);

    public static readonly GetUserTemplatesRequestType Ejs = new(Values.Ejs);

    public static readonly GetUserTemplatesRequestType Html = new(Values.Html);

    public static readonly GetUserTemplatesRequestType Latex = new(Values.Latex);

    public static readonly GetUserTemplatesRequestType Framework = new(Values.Framework);

    public GetUserTemplatesRequestType(string value)
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
    public static GetUserTemplatesRequestType FromCustom(string value)
    {
        return new GetUserTemplatesRequestType(value);
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

    public static bool operator ==(GetUserTemplatesRequestType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GetUserTemplatesRequestType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GetUserTemplatesRequestType value) => value.Value;

    public static explicit operator GetUserTemplatesRequestType(string value) => new(value);

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

        public const string Framework = "framework";
    }
}
