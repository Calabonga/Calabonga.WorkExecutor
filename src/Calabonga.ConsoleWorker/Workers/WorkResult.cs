namespace Calabonga.ConsoleWorker.Workers;

/// <summary>
/// Base class for work result
/// </summary>
public abstract class WorkResult<T> : IWorkResult<T>
{
    /// <summary>
    /// Indicates that the work is fired
    /// </summary>
    protected abstract bool IsFired { get; }

    /// <summary>
    /// Work messages text
    /// </summary>
    protected abstract IEnumerable<string> Errors { get; }

    /// <summary>
    /// WorkResult that has been obtained
    /// </summary>
    public T? Result { get; protected init; }

    /// <summary>
    /// Indicates work successfully completed and the WorkResult has been obtained
    /// </summary>
    public bool IsSuccess => IsFired && !Errors.Any() && Result is not null;
}
