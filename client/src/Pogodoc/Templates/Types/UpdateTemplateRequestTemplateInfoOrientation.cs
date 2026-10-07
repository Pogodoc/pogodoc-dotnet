using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<UpdateTemplateRequestTemplateInfoOrientation>))]
[Serializable]
public readonly record struct UpdateTemplateRequestTemplateInfoOrientation : IStringEnum
{
    public static readonly UpdateTemplateRequestTemplateInfoOrientation Landscape = new(
        Values.Landscape
    );

    public static readonly UpdateTemplateRequestTemplateInfoOrientation Portrait = new(
        Values.Portrait
    );

    public UpdateTemplateRequestTemplateInfoOrientation(string value)
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
    public static UpdateTemplateRequestTemplateInfoOrientation FromCustom(string value)
    {
        return new UpdateTemplateRequestTemplateInfoOrientation(value);
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
        UpdateTemplateRequestTemplateInfoOrientation value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateTemplateRequestTemplateInfoOrientation value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateTemplateRequestTemplateInfoOrientation value) =>
        value.Value;

    public static explicit operator UpdateTemplateRequestTemplateInfoOrientation(string value) =>
        new(value);

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
