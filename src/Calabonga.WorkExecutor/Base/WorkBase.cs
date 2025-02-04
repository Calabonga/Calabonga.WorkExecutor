using Calabonga.WorkExecutor.Exceptions;
using Calabonga.WorkExecutor.Results;
using Calabonga.WorkExecutor.Results.Base;
using Microsoft.Extensions.Logging;

namespace Calabonga.WorkExecutor.Base;

/// <summary>
///Default <see cref="IWork{T}"/> implementation with general functionality
/// </summary>
/// <typeparam name="TResult"></typeparam>
public abstract class WorkBase<TResult> : IWork<TResult> where TResult : class
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
    public string Name => ((IWork)this).GetName();

    /// <summary>
    /// Runs current work
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<IWorkReport<TResult>> RunWorkAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Internal execution for <see cref="IWork"/> <see cref="RunWorkAsync"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    protected internal async Task<IWorkReport<TResult>> ExecuteWorkAsync<TExecutor>(CancellationToken cancellationToken, TExecutor executor)
        where TExecutor : IWorkExecutor<TResult>
    {
        try
        {
            executor.Logger.LogDebug("[EXECUTOR] Executing {Name}...", ((IWork)this).GetName());
            var result = await RunWorkAsync(cancellationToken).ConfigureAwait(false);
            executor.Logger.LogDebug("[EXECUTOR] {Name} completed.", ((IWork)this).GetName());
            executor.SetResult(result);

            return result;
        }
        catch (OperationCanceledException)
        {
            executor.Logger.LogDebug("[EXECUTOR] {Name} cancelled.", ((IWork)this).GetName());
            executor.SetResult(new WorkErrorReport<TResult>([$"{Name} cancelled by timeout."], this));
            return new WorkCancelledReport<TResult>(this);
        }
        catch (Exception exception)
        {
            executor.Logger.LogDebug("[EXECUTOR] {Name} failed.", ((IWork)this).GetName());
            executor.SetResult(new WorkFailedReport<TResult>(new WorkerFailedException($"{Name} failed: {exception.Message}", exception), this));
            return new WorkFailedReport<TResult>(exception, this);
        }
    }
}
