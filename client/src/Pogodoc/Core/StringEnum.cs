using System.Text.Json.Serialization;

namespace Pogodoc.Core;

public interface IStringEnum : IEquatable<string>
{
    public string Value { get; }
}
