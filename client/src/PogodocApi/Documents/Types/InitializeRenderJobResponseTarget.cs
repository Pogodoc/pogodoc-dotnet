using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<InitializeRenderJobResponseTarget>))]
[Serializable]
public readonly record struct InitializeRenderJobResponseTarget : IStringEnum
{
    public static readonly InitializeRenderJobResponseTarget Pdf = new(Values.Pdf);

    public static readonly InitializeRenderJobResponseTarget Html = new(Values.Html);

    public static readonly InitializeRenderJobResponseTarget Docx = new(Values.Docx);

    public static readonly InitializeRenderJobResponseTarget Xlsx = new(Values.Xlsx);

    public static readonly InitializeRenderJobResponseTarget Pptx = new(Values.Pptx);

    public static readonly InitializeRenderJobResponseTarget Png = new(Values.Png);

    public static readonly InitializeRenderJobResponseTarget Jpg = new(Values.Jpg);

    public InitializeRenderJobResponseTarget(string value)
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
    public static InitializeRenderJobResponseTarget FromCustom(string value)
    {
        return new InitializeRenderJobResponseTarget(value);
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

    public static bool operator ==(InitializeRenderJobResponseTarget value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InitializeRenderJobResponseTarget value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InitializeRenderJobResponseTarget value) => value.Value;

    public static explicit operator InitializeRenderJobResponseTarget(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Pdf = "pdf";

        public const string Html = "html";

        public const string Docx = "docx";

        public const string Xlsx = "xlsx";

        public const string Pptx = "pptx";

        public const string Png = "png";

        public const string Jpg = "jpg";
    }
}
