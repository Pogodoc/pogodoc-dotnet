using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<GetTemplateByIdResponsePermissions>))]
[Serializable]
public readonly record struct GetTemplateByIdResponsePermissions : IStringEnum
{
    public static readonly GetTemplateByIdResponsePermissions Public = new(Values.Public);

    public static readonly GetTemplateByIdResponsePermissions Private = new(Values.Private);

    public GetTemplateByIdResponsePermissions(string value)
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
    public static GetTemplateByIdResponsePermissions FromCustom(string value)
    {
        return new GetTemplateByIdResponsePermissions(value);
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

    public static bool operator ==(GetTemplateByIdResponsePermissions value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GetTemplateByIdResponsePermissions value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GetTemplateByIdResponsePermissions value) => value.Value;

    public static explicit operator GetTemplateByIdResponsePermissions(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Public = "public";

        public const string Private = "private";
    }
}
