using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(EnumSerializer<GetJobStatusResponseType>))]
public enum GetJobStatusResponseType
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
    React,
}
