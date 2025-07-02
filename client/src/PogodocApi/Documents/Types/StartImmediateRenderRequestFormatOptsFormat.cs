using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<StartImmediateRenderRequestFormatOptsFormat>))]
[Serializable]
public readonly record struct StartImmediateRenderRequestFormatOptsFormat : IStringEnum
{
    public static readonly StartImmediateRenderRequestFormatOptsFormat Letter = new(Values.Letter);

    public static readonly StartImmediateRenderRequestFormatOptsFormat Legal = new(Values.Legal);

    public static readonly StartImmediateRenderRequestFormatOptsFormat Tabloid = new(
        Values.Tabloid
    );

    public static readonly StartImmediateRenderRequestFormatOptsFormat Ledger = new(Values.Ledger);

    public static readonly StartImmediateRenderRequestFormatOptsFormat A0 = new(Values.A0);

    public static readonly StartImmediateRenderRequestFormatOptsFormat A1 = new(Values.A1);

    public static readonly StartImmediateRenderRequestFormatOptsFormat A2 = new(Values.A2);

    public static readonly StartImmediateRenderRequestFormatOptsFormat A3 = new(Values.A3);

    public static readonly StartImmediateRenderRequestFormatOptsFormat A4 = new(Values.A4);

    public static readonly StartImmediateRenderRequestFormatOptsFormat A5 = new(Values.A5);

    public static readonly StartImmediateRenderRequestFormatOptsFormat A6 = new(Values.A6);

    public StartImmediateRenderRequestFormatOptsFormat(string value)
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
    public static StartImmediateRenderRequestFormatOptsFormat FromCustom(string value)
    {
        return new StartImmediateRenderRequestFormatOptsFormat(value);
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
        StartImmediateRenderRequestFormatOptsFormat value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        StartImmediateRenderRequestFormatOptsFormat value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(StartImmediateRenderRequestFormatOptsFormat value) =>
        value.Value;

    public static explicit operator StartImmediateRenderRequestFormatOptsFormat(string value) =>
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
