using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<UpdateTemplateRequestTemplateInfoType>))]
[Serializable]
public readonly record struct UpdateTemplateRequestTemplateInfoType : IStringEnum
{
    public static readonly UpdateTemplateRequestTemplateInfoType Docx = new(Values.Docx);

    public static readonly UpdateTemplateRequestTemplateInfoType Xlsx = new(Values.Xlsx);

    public static readonly UpdateTemplateRequestTemplateInfoType Pptx = new(Values.Pptx);

    public static readonly UpdateTemplateRequestTemplateInfoType Ejs = new(Values.Ejs);

    public static readonly UpdateTemplateRequestTemplateInfoType Html = new(Values.Html);

    public static readonly UpdateTemplateRequestTemplateInfoType Latex = new(Values.Latex);

    public static readonly UpdateTemplateRequestTemplateInfoType React = new(Values.React);

    public UpdateTemplateRequestTemplateInfoType(string value)
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
    public static UpdateTemplateRequestTemplateInfoType FromCustom(string value)
    {
        return new UpdateTemplateRequestTemplateInfoType(value);
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

    public static bool operator ==(UpdateTemplateRequestTemplateInfoType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UpdateTemplateRequestTemplateInfoType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UpdateTemplateRequestTemplateInfoType value) =>
        value.Value;

    public static explicit operator UpdateTemplateRequestTemplateInfoType(string value) =>
        new(value);

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
