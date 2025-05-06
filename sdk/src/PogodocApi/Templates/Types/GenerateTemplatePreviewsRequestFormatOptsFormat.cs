using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PogodocApi;
using PogodocApi.Core;

#nullable enable

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<GenerateTemplatePreviewsRequestFormatOptsFormat>))]
public enum GenerateTemplatePreviewsRequestFormatOptsFormat
{
    [EnumMember(Value = "letter")]
    Letter,

    [EnumMember(Value = "legal")]
    Legal,

    [EnumMember(Value = "tabloid")]
    Tabloid,

    [EnumMember(Value = "ledger")]
    Ledger,

    [EnumMember(Value = "a0")]
    A0,

    [EnumMember(Value = "a1")]
    A1,

    [EnumMember(Value = "a2")]
    A2,

    [EnumMember(Value = "a3")]
    A3,

    [EnumMember(Value = "a4")]
    A4,

    [EnumMember(Value = "a5")]
    A5,

    [EnumMember(Value = "a6")]
    A6
}
