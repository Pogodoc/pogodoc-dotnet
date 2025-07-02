using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<SaveCreatedTemplateRequestTemplateInfoType>))]
[Serializable]
public readonly record struct SaveCreatedTemplateRequestTemplateInfoType : IStringEnum
{
    public static readonly SaveCreatedTemplateRequestTemplateInfoType Docx = new(Values.Docx);

    public static readonly SaveCreatedTemplateRequestTemplateInfoType Xlsx = new(Values.Xlsx);

    public static readonly SaveCreatedTemplateRequestTemplateInfoType Pptx = new(Values.Pptx);

    public static readonly SaveCreatedTemplateRequestTemplateInfoType Ejs = new(Values.Ejs);

    public static readonly SaveCreatedTemplateRequestTemplateInfoType Html = new(Values.Html);

    public static readonly SaveCreatedTemplateRequestTemplateInfoType Latex = new(Values.Latex);

    public static readonly SaveCreatedTemplateRequestTemplateInfoType React = new(Values.React);

    public SaveCreatedTemplateRequestTemplateInfoType(string value)
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
    public static SaveCreatedTemplateRequestTemplateInfoType FromCustom(string value)
    {
        return new SaveCreatedTemplateRequestTemplateInfoType(value);
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
        SaveCreatedTemplateRequestTemplateInfoType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        SaveCreatedTemplateRequestTemplateInfoType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(SaveCreatedTemplateRequestTemplateInfoType value) =>
        value.Value;

    public static explicit operator SaveCreatedTemplateRequestTemplateInfoType(string value) =>
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
