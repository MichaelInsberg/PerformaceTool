namespace PerformanceTool.PerformanceTool.Exceptions;

/// <summary>
/// The concurrent dictionary exception class
/// </summary>
public sealed class ConcurrentDictionaryException : Exception
{
    /// <inheritdoc />
    public ConcurrentDictionaryException()
    {
    }

    /// <inheritdoc />
    public ConcurrentDictionaryException(string message) : base(message)
    {
    }

    /// <inheritdoc />
    public ConcurrentDictionaryException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

