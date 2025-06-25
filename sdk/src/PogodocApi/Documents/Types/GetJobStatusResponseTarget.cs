using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<GetJobStatusResponseTarget>))]
[Serializable]
public readonly record struct GetJobStatusResponseTarget : IStringEnum
{
    public static readonly GetJobStatusResponseTarget Pdf = new(Values.Pdf);

    public static readonly GetJobStatusResponseTarget Html = new(Values.Html);

    public static readonly GetJobStatusResponseTarget Docx = new(Values.Docx);

    public static readonly GetJobStatusResponseTarget Xlsx = new(Values.Xlsx);

    public static readonly GetJobStatusResponseTarget Pptx = new(Values.Pptx);

    public static readonly GetJobStatusResponseTarget Png = new(Values.Png);

    public static readonly GetJobStatusResponseTarget Jpg = new(Values.Jpg);

    public GetJobStatusResponseTarget(string value)
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
    public static GetJobStatusResponseTarget FromCustom(string value)
    {
        return new GetJobStatusResponseTarget(value);
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

    public static bool operator ==(GetJobStatusResponseTarget value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GetJobStatusResponseTarget value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GetJobStatusResponseTarget value) => value.Value;

    public static explicit operator GetJobStatusResponseTarget(string value) => new(value);

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
