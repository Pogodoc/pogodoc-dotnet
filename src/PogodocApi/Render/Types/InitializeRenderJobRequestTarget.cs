using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PogodocApi;
using PogodocApi.Core;

#nullable enable

namespace PogodocApi;

[JsonConverter(typeof(StringEnumSerializer<InitializeRenderJobRequestTarget>))]
public enum InitializeRenderJobRequestTarget
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
    Jpg
}
