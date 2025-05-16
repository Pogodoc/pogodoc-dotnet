namespace PogodocApi.Core;

public interface IIsRetryableContent
{
    public bool IsRetryable { get; }
}
