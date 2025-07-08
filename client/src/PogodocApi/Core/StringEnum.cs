using System.Text.Json.Serialization;

namespace PogodocApi.Core;

public interface IStringEnum : IEquatable<string>
{
    public string Value { get; }
}
