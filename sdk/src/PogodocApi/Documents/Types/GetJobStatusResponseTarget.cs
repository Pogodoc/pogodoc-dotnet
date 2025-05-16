using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PogodocApi.Core;

namespace PogodocApi;

[JsonConverter(typeof(EnumSerializer<GetJobStatusResponseTarget>))]
public enum GetJobStatusResponseTarget
{
    [EnumMember(Value = "pdf")]
    Pdf,

    [EnumMember(Value = "html")]
    Html,

    [EnumMember(Value = "docx")]
    Docx,

    [EnumMember(Value = "xlsx")]
    Xlsx,

    [EnumMember(Value = "pptx")]
    Pptx,

    [EnumMember(Value = "png")]
    Png,

    [EnumMember(Value = "jpg")]
    Jpg,
}
