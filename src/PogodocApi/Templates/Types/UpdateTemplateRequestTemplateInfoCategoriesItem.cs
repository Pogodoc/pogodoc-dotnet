using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PogodocApi;
using PogodocApi.Core;

#nullable enable

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<UpdateTemplateRequestTemplateInfoCategoriesItem>))]
public enum UpdateTemplateRequestTemplateInfoCategoriesItem
{
    [EnumMember(Value = "invoice")]
    Invoice,

    [EnumMember(Value = "mail")]
    Mail,

    [EnumMember(Value = "report")]
    Report,

    [EnumMember(Value = "cv")]
    Cv,

    [EnumMember(Value = "other")]
    Other
}
