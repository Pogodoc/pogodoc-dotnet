using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<StartRenderJobResponseTarget>))]
[Serializable]
public readonly record struct StartRenderJobResponseTarget : IStringEnum
{
    public static readonly StartRenderJobResponseTarget Pdf = new(Values.Pdf);

    public static readonly StartRenderJobResponseTarget Html = new(Values.Html);

    public static readonly StartRenderJobResponseTarget Docx = new(Values.Docx);

    public static readonly StartRenderJobResponseTarget Xlsx = new(Values.Xlsx);

    public static readonly StartRenderJobResponseTarget Pptx = new(Values.Pptx);

    public static readonly StartRenderJobResponseTarget Png = new(Values.Png);

    public static readonly StartRenderJobResponseTarget Jpg = new(Values.Jpg);

    public StartRenderJobResponseTarget(string value)
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
    public static StartRenderJobResponseTarget FromCustom(string value)
    {
        return new StartRenderJobResponseTarget(value);
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

    public static bool operator ==(StartRenderJobResponseTarget value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(StartRenderJobResponseTarget value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(StartRenderJobResponseTarget value) => value.Value;

    public static explicit operator StartRenderJobResponseTarget(string value) => new(value);

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
