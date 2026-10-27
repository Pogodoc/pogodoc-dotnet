using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

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

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Receipt = new(
        Values.Receipt
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Order = new(
        Values.Order
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Contract = new(
        Values.Contract
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Certificate = new(
        Values.Certificate
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Statement = new(
        Values.Statement
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Brochure = new(
        Values.Brochure
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Warranty = new(
        Values.Warranty
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Poster = new(
        Values.Poster
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Menu = new(
        Values.Menu
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Catalog = new(
        Values.Catalog
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Packaging = new(
        Values.Packaging
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Advertisement = new(
        Values.Advertisement
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Other = new(
        Values.Other
    );

    public static readonly SaveCreatedTemplateRequestTemplateInfoCategoriesItem Favorite = new(
        Values.Favorite
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

        public const string Receipt = "receipt";

        public const string Order = "order";

        public const string Contract = "contract";

        public const string Certificate = "certificate";

        public const string Statement = "statement";

        public const string Brochure = "brochure";

        public const string Warranty = "warranty";

        public const string Poster = "poster";

        public const string Menu = "menu";

        public const string Catalog = "catalog";

        public const string Packaging = "packaging";

        public const string Advertisement = "advertisement";

        public const string Other = "other";

        public const string Favorite = "favorite";
    }
}
