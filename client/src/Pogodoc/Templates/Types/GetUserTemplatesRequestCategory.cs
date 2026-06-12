using System.Text.Json.Serialization;
using Pogodoc.Core;

namespace Pogodoc;

[JsonConverter(typeof(StringEnumSerializer<GetUserTemplatesRequestCategory>))]
[Serializable]
public readonly record struct GetUserTemplatesRequestCategory : IStringEnum
{
    public static readonly GetUserTemplatesRequestCategory Invoice = new(Values.Invoice);

    public static readonly GetUserTemplatesRequestCategory Mail = new(Values.Mail);

    public static readonly GetUserTemplatesRequestCategory Report = new(Values.Report);

    public static readonly GetUserTemplatesRequestCategory Cv = new(Values.Cv);

    public static readonly GetUserTemplatesRequestCategory Receipt = new(Values.Receipt);

    public static readonly GetUserTemplatesRequestCategory Order = new(Values.Order);

    public static readonly GetUserTemplatesRequestCategory Contract = new(Values.Contract);

    public static readonly GetUserTemplatesRequestCategory Certificate = new(Values.Certificate);

    public static readonly GetUserTemplatesRequestCategory Statement = new(Values.Statement);

    public static readonly GetUserTemplatesRequestCategory Brochure = new(Values.Brochure);

    public static readonly GetUserTemplatesRequestCategory Warranty = new(Values.Warranty);

    public static readonly GetUserTemplatesRequestCategory Poster = new(Values.Poster);

    public static readonly GetUserTemplatesRequestCategory Menu = new(Values.Menu);

    public static readonly GetUserTemplatesRequestCategory Catalog = new(Values.Catalog);

    public static readonly GetUserTemplatesRequestCategory Packaging = new(Values.Packaging);

    public static readonly GetUserTemplatesRequestCategory Advertisement = new(
        Values.Advertisement
    );

    public static readonly GetUserTemplatesRequestCategory Other = new(Values.Other);

    public static readonly GetUserTemplatesRequestCategory Favorite = new(Values.Favorite);

    public GetUserTemplatesRequestCategory(string value)
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
    public static GetUserTemplatesRequestCategory FromCustom(string value)
    {
        return new GetUserTemplatesRequestCategory(value);
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

    public static bool operator ==(GetUserTemplatesRequestCategory value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GetUserTemplatesRequestCategory value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GetUserTemplatesRequestCategory value) => value.Value;

    public static explicit operator GetUserTemplatesRequestCategory(string value) => new(value);

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
