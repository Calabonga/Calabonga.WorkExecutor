namespace Calabonga.ConsoleWorker.Workers;

/// <summary>
/// // Calabonga: Summary required (WorkBase 2025-02-02 09:36)
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class WorkBase<T> : IWork<T>
{
    /// <summary>
    /// Index sorting
    /// </summary>
    public abstract int OrderIndex { get; }

    /// <summary>
    /// Name for the rule
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public abstract string DisplayName { get; }

    /// <summary>
    /// Timeout after that current work become expired (failed)
    /// </summary>
    public virtual TimeSpan Timeout { get; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Runs current work
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<IWorkResult<T>> RunWorkAsync(CancellationToken cancellationToken);
}
