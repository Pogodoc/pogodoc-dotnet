using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<InitializeRenderJobRequestTarget>))]
[Serializable]
public readonly record struct InitializeRenderJobRequestTarget : IStringEnum
{
    public static readonly InitializeRenderJobRequestTarget Pdf = new(Values.Pdf);

    public static readonly InitializeRenderJobRequestTarget Html = new(Values.Html);

    public static readonly InitializeRenderJobRequestTarget Docx = new(Values.Docx);

    public static readonly InitializeRenderJobRequestTarget Xlsx = new(Values.Xlsx);

    public static readonly InitializeRenderJobRequestTarget Pptx = new(Values.Pptx);

    public static readonly InitializeRenderJobRequestTarget Png = new(Values.Png);

    public static readonly InitializeRenderJobRequestTarget Jpg = new(Values.Jpg);

    public InitializeRenderJobRequestTarget(string value)
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
    public static InitializeRenderJobRequestTarget FromCustom(string value)
    {
        return new InitializeRenderJobRequestTarget(value);
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

    public static bool operator ==(InitializeRenderJobRequestTarget value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InitializeRenderJobRequestTarget value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InitializeRenderJobRequestTarget value) => value.Value;

    public static explicit operator InitializeRenderJobRequestTarget(string value) => new(value);

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
