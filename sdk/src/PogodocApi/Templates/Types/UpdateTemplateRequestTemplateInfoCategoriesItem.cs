using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<UpdateTemplateRequestTemplateInfoCategoriesItem>))]
[Serializable]
public readonly record struct UpdateTemplateRequestTemplateInfoCategoriesItem : IStringEnum
{
    public static readonly UpdateTemplateRequestTemplateInfoCategoriesItem Invoice = new(
        Values.Invoice
    );

    public static readonly UpdateTemplateRequestTemplateInfoCategoriesItem Mail = new(Values.Mail);

    public static readonly UpdateTemplateRequestTemplateInfoCategoriesItem Report = new(
        Values.Report
    );

    public static readonly UpdateTemplateRequestTemplateInfoCategoriesItem Cv = new(Values.Cv);

    public static readonly UpdateTemplateRequestTemplateInfoCategoriesItem Other = new(
        Values.Other
    );

    public UpdateTemplateRequestTemplateInfoCategoriesItem(string value)
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
    public static UpdateTemplateRequestTemplateInfoCategoriesItem FromCustom(string value)
    {
        return new UpdateTemplateRequestTemplateInfoCategoriesItem(value);
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
        UpdateTemplateRequestTemplateInfoCategoriesItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateTemplateRequestTemplateInfoCategoriesItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateTemplateRequestTemplateInfoCategoriesItem value) =>
        value.Value;

    public static explicit operator UpdateTemplateRequestTemplateInfoCategoriesItem(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Invoice = "invoice";

        public const string Mail = "mail";

        public const string Report = "report";

        public const string Cv = "cv";

        public const string Other = "other";
    }
}
