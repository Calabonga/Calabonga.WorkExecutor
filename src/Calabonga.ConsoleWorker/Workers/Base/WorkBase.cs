using Calabonga.ConsoleWorker.Workers.Results;
using Calabonga.ConsoleWorker.Workers.Results.Base;
using Microsoft.Extensions.Logging;

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
    /// Friendly name
    /// </summary>
    public abstract string DisplayName { get; }

    /// <summary>
    /// Timeout after that current work become expired (failed)
    /// </summary>
    public virtual TimeSpan Timeout { get; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Name for the rule
    /// </summary>
    public virtual string Name => $"{GetType().Name}[{Timeout}]";

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
    /// <param name="logger"></param>
    /// <returns></returns>
    public async Task<IWorkReport<T>> ExecuteWorkAsync(CancellationToken cancellationToken, ILogger logger)
    {
        var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        try
        {
            logger.LogDebug("Executing work {Name} ({Timeout})...", Name, Timeout);
            var result = await RunWorkAsync(cancellationTokenSource.Token);
            logger.LogDebug("Work {Name} ({Timeout}) completed.", Name, Timeout);
            return result;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("Work {Name} ({Timeout}) cancelled.", Name, Timeout);
            return new WorkCancelledReport<T>(this);
        }
        catch (Exception exception)
        {
            logger.LogDebug("Work {Name} ({Timeout}) failed.", Name, Timeout);
            return new WorkFailedReport<T>(exception, this);
        }
    }
}
