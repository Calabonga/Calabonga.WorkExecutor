namespace Calabonga.WorkExecutor.Exceptions;

/// <summary>
/// WorkExecutor fail exception
/// </summary>
public class WorkExecutorException : InvalidOperationException
{
    public WorkExecutorException() { }

    public WorkExecutorException(string? message) : base(message) { }

    public WorkExecutorException(string? message, Exception? innerException) : base(message, innerException) { }
}
