namespace Calabonga.WorkExecutor.Exceptions;

/// <summary>
/// WorkExecutor fail exception
/// </summary>
public class WorkerFailedException : InvalidOperationException
{
    public WorkerFailedException() { }

    public WorkerFailedException(string? message) : base(message) { }

    public WorkerFailedException(string? message, Exception? innerException) : base(message, innerException) { }
}