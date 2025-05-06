using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PogodocApi;
using PogodocApi.Core;

#nullable enable

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<StartImmediateRenderRequestType>))]
public enum StartImmediateRenderRequestType
{
    [EnumMember(Value = "docx")]
    Docx,

    [EnumMember(Value = "xlsx")]
    Xlsx,

    [EnumMember(Value = "pptx")]
    Pptx,

    [EnumMember(Value = "ejs")]
    Ejs,

    [EnumMember(Value = "html")]
    Html,

    [EnumMember(Value = "latex")]
    Latex,

    [EnumMember(Value = "react")]
    React
}
