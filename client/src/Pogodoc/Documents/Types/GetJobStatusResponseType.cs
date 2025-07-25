using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<GetJobStatusResponseType>))]
[Serializable]
public readonly record struct GetJobStatusResponseType : IStringEnum
{
    public static readonly GetJobStatusResponseType Docx = new(Values.Docx);

    public static readonly GetJobStatusResponseType Xlsx = new(Values.Xlsx);

    public static readonly GetJobStatusResponseType Pptx = new(Values.Pptx);

    public static readonly GetJobStatusResponseType Ejs = new(Values.Ejs);

    public static readonly GetJobStatusResponseType Html = new(Values.Html);

    public static readonly GetJobStatusResponseType Latex = new(Values.Latex);

    public static readonly GetJobStatusResponseType React = new(Values.React);

    public GetJobStatusResponseType(string value)
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
    public static GetJobStatusResponseType FromCustom(string value)
    {
        return new GetJobStatusResponseType(value);
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

    public static bool operator ==(GetJobStatusResponseType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GetJobStatusResponseType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GetJobStatusResponseType value) => value.Value;

    public static explicit operator GetJobStatusResponseType(string value) => new(value);

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
