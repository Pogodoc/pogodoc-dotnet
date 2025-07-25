using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<StartImmediateRenderRequestTarget>))]
[Serializable]
public readonly record struct StartImmediateRenderRequestTarget : IStringEnum
{
    public static readonly StartImmediateRenderRequestTarget Pdf = new(Values.Pdf);

    public static readonly StartImmediateRenderRequestTarget Html = new(Values.Html);

    public static readonly StartImmediateRenderRequestTarget Docx = new(Values.Docx);

    public static readonly StartImmediateRenderRequestTarget Xlsx = new(Values.Xlsx);

    public static readonly StartImmediateRenderRequestTarget Pptx = new(Values.Pptx);

    public static readonly StartImmediateRenderRequestTarget Png = new(Values.Png);

    public static readonly StartImmediateRenderRequestTarget Jpg = new(Values.Jpg);

    public StartImmediateRenderRequestTarget(string value)
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
    public static StartImmediateRenderRequestTarget FromCustom(string value)
    {
        return new StartImmediateRenderRequestTarget(value);
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

    public static bool operator ==(StartImmediateRenderRequestTarget value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(StartImmediateRenderRequestTarget value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(StartImmediateRenderRequestTarget value) => value.Value;

    public static explicit operator StartImmediateRenderRequestTarget(string value) => new(value);

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
