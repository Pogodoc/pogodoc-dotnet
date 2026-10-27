using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<GetUserTemplatesRequestSort>))]
[Serializable]
public readonly record struct GetUserTemplatesRequestSort : IStringEnum
{
    public static readonly GetUserTemplatesRequestSort CreatedAtDesc = new(Values.CreatedAtDesc);

    public static readonly GetUserTemplatesRequestSort CreatedAtAsc = new(Values.CreatedAtAsc);

    public static readonly GetUserTemplatesRequestSort UpdatedAtDesc = new(Values.UpdatedAtDesc);

    public static readonly GetUserTemplatesRequestSort UpdatedAtAsc = new(Values.UpdatedAtAsc);

    public static readonly GetUserTemplatesRequestSort TitleAsc = new(Values.TitleAsc);

    public static readonly GetUserTemplatesRequestSort TitleDesc = new(Values.TitleDesc);

    public GetUserTemplatesRequestSort(string value)
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
    public static GetUserTemplatesRequestSort FromCustom(string value)
    {
        return new GetUserTemplatesRequestSort(value);
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

    public static bool operator ==(GetUserTemplatesRequestSort value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GetUserTemplatesRequestSort value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GetUserTemplatesRequestSort value) => value.Value;

    public static explicit operator GetUserTemplatesRequestSort(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CreatedAtDesc = "createdAt:desc";

        public const string CreatedAtAsc = "createdAt:asc";

        public const string UpdatedAtDesc = "updatedAt:desc";

        public const string UpdatedAtAsc = "updatedAt:asc";

        public const string TitleAsc = "title:asc";

        public const string TitleDesc = "title:desc";
    }
}
