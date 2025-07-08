namespace PogodocApi;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class PogodocApiException(string message, Exception? innerException = null)
    : Exception(message, innerException);
