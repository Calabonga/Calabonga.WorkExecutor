using Calabonga.ConsoleWorker.Workers.Results;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.Workers.Base;

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
    public abstract Task<IWorkReport<T>> RunWorkAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Internal execution for <see cref="IWork"/> <see cref="RunWorkAsync"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IWorkReport<T>> ExecuteWorkAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await RunWorkAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return new WorkCancelledReport<T>(this);
        }
        catch (Exception exception)
        {
            return new WorkFailedReport<T>(exception, this);
        }
    }
}
