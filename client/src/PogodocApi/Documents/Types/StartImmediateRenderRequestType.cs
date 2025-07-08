using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<StartImmediateRenderRequestType>))]
[Serializable]
public readonly record struct StartImmediateRenderRequestType : IStringEnum
{
    public static readonly StartImmediateRenderRequestType Docx = new(Values.Docx);

    public static readonly StartImmediateRenderRequestType Xlsx = new(Values.Xlsx);

    public static readonly StartImmediateRenderRequestType Pptx = new(Values.Pptx);

    public static readonly StartImmediateRenderRequestType Ejs = new(Values.Ejs);

    public static readonly StartImmediateRenderRequestType Html = new(Values.Html);

    public static readonly StartImmediateRenderRequestType Latex = new(Values.Latex);

    public static readonly StartImmediateRenderRequestType React = new(Values.React);

    public StartImmediateRenderRequestType(string value)
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
    public static StartImmediateRenderRequestType FromCustom(string value)
    {
        return new StartImmediateRenderRequestType(value);
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

    public static bool operator ==(StartImmediateRenderRequestType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(StartImmediateRenderRequestType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(StartImmediateRenderRequestType value) => value.Value;

    public static explicit operator StartImmediateRenderRequestType(string value) => new(value);

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
