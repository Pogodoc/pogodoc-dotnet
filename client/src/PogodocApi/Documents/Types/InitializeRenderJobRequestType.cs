using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<InitializeRenderJobRequestType>))]
[Serializable]
public readonly record struct InitializeRenderJobRequestType : IStringEnum
{
    public static readonly InitializeRenderJobRequestType Docx = new(Values.Docx);

    public static readonly InitializeRenderJobRequestType Xlsx = new(Values.Xlsx);

    public static readonly InitializeRenderJobRequestType Pptx = new(Values.Pptx);

    public static readonly InitializeRenderJobRequestType Ejs = new(Values.Ejs);

    public static readonly InitializeRenderJobRequestType Html = new(Values.Html);

    public static readonly InitializeRenderJobRequestType Latex = new(Values.Latex);

    public static readonly InitializeRenderJobRequestType React = new(Values.React);

    public InitializeRenderJobRequestType(string value)
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
    public static InitializeRenderJobRequestType FromCustom(string value)
    {
        return new InitializeRenderJobRequestType(value);
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

    public static bool operator ==(InitializeRenderJobRequestType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InitializeRenderJobRequestType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InitializeRenderJobRequestType value) => value.Value;

    public static explicit operator InitializeRenderJobRequestType(string value) => new(value);

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
