using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<SaveCreatedTemplateRequestTemplateInfoOrientation>))]
[Serializable]
public readonly record struct SaveCreatedTemplateRequestTemplateInfoOrientation : IStringEnum
{
    public static readonly SaveCreatedTemplateRequestTemplateInfoOrientation Landscape = new(
        Values.Landscape
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoOrientation Portrait = new(
        Values.Portrait
    );

    public SaveCreatedTemplateRequestTemplateInfoOrientation(string value)
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
    public static SaveCreatedTemplateRequestTemplateInfoOrientation FromCustom(string value)
    {
        return new SaveCreatedTemplateRequestTemplateInfoOrientation(value);
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
        SaveCreatedTemplateRequestTemplateInfoOrientation value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        SaveCreatedTemplateRequestTemplateInfoOrientation value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        SaveCreatedTemplateRequestTemplateInfoOrientation value
    ) => value.Value;

    public static explicit operator SaveCreatedTemplateRequestTemplateInfoOrientation(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Landscape = "landscape";

        public const string Portrait = "portrait";
    }
}
