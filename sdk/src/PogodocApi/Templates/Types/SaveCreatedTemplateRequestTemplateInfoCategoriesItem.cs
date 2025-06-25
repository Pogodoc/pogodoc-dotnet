using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<SaveCreatedTemplateRequestTemplateInfoCategoriesItem>))]
[Serializable]
public readonly record struct SaveCreatedTemplateRequestTemplateInfoCategoriesItem : IStringEnum
{
    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Invoice = new(
        Values.Invoice
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Mail = new(
        Values.Mail
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Report = new(
        Values.Report
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Cv = new(Values.Cv);

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Other = new(
        Values.Other
    );

    public SaveCreatedTemplateRequestTemplateInfoCategoriesItem(string value)
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
    public static SaveCreatedTemplateRequestTemplateInfoCategoriesItem FromCustom(string value)
    {
        return new SaveCreatedTemplateRequestTemplateInfoCategoriesItem(value);
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
        SaveCreatedTemplateRequestTemplateInfoCategoriesItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        SaveCreatedTemplateRequestTemplateInfoCategoriesItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        SaveCreatedTemplateRequestTemplateInfoCategoriesItem value
    ) => value.Value;

    public static explicit operator SaveCreatedTemplateRequestTemplateInfoCategoriesItem(
        string value
    ) => new(value);

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
