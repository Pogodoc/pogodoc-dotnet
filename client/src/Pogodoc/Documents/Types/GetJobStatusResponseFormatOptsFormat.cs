using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<GetJobStatusResponseFormatOptsFormat>))]
[Serializable]
public readonly record struct GetJobStatusResponseFormatOptsFormat : IStringEnum
{
    public static readonly GetJobStatusResponseFormatOptsFormat Letter = new(Values.Letter);

    public static readonly GetJobStatusResponseFormatOptsFormat Legal = new(Values.Legal);

    public static readonly GetJobStatusResponseFormatOptsFormat Tabloid = new(Values.Tabloid);

    public static readonly GetJobStatusResponseFormatOptsFormat Ledger = new(Values.Ledger);

    public static readonly GetJobStatusResponseFormatOptsFormat A0 = new(Values.A0);

    public static readonly GetJobStatusResponseFormatOptsFormat A1 = new(Values.A1);

    public static readonly GetJobStatusResponseFormatOptsFormat A2 = new(Values.A2);

    public static readonly GetJobStatusResponseFormatOptsFormat A3 = new(Values.A3);

    public static readonly GetJobStatusResponseFormatOptsFormat A4 = new(Values.A4);

    public static readonly GetJobStatusResponseFormatOptsFormat A5 = new(Values.A5);

    public static readonly GetJobStatusResponseFormatOptsFormat A6 = new(Values.A6);

    public GetJobStatusResponseFormatOptsFormat(string value)
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
    public static GetJobStatusResponseFormatOptsFormat FromCustom(string value)
    {
        return new GetJobStatusResponseFormatOptsFormat(value);
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

    public static bool operator ==(GetJobStatusResponseFormatOptsFormat value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GetJobStatusResponseFormatOptsFormat value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GetJobStatusResponseFormatOptsFormat value) =>
        value.Value;

    public static explicit operator GetJobStatusResponseFormatOptsFormat(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Letter = "letter";

        public const string Legal = "legal";

        public const string Tabloid = "tabloid";

        public const string Ledger = "ledger";

        public const string A0 = "a0";

        public const string A1 = "a1";

        public const string A2 = "a2";

        public const string A3 = "a3";

        public const string A4 = "a4";

        public const string A5 = "a5";

        public const string A6 = "a6";
    }
}
