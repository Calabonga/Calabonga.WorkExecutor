using Calabonga.WorkExecutor.Exceptions;
using Calabonga.WorkExecutor.Reports;
using Calabonga.WorkExecutor.Reports.Base;
using Microsoft.Extensions.Logging;

namespace Calabonga.WorkExecutor.Base;

///  <summary>
/// Default <see cref="IWork{T}"/> implementation with general functionality
///  </summary>
///  <typeparam name="TResult"></typeparam>
public abstract class WorkBase<TResult> : IWork<TResult>
    where TResult : class
{
    protected WorkBase()
    {
        PrepareMetadata();
    }

    /// <summary>
    /// Index sorting
    /// </summary>
    public abstract int OrderIndex { get; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public abstract string? DisplayName { get; }

    /// <summary>
    /// Timeout after that current work become expired (failed)
    /// </summary>
    public virtual TimeSpan Timeout { get; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Name for the rule
    /// </summary>
    public string Name => ((IWork)this).GetName();

    /// <summary>
    /// Metadata for work customization
    /// </summary>
    public IWorkMetadata? Metadata { get; private set; }

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
        catch (TaskCanceledException)
        {
            executor.Logger.LogDebug("[EXECUTOR] {Name} cancelled.", ((IWork)this).GetName());
            var cancelled = new WorkCancelledReport<TResult>(this);
            executor.SetResult(cancelled);
            return cancelled;
        }
        catch (Exception exception)
        {
            executor.Logger.LogDebug("[EXECUTOR] {Name} failed.", ((IWork)this).GetName());
            var error = new WorkFailedReport<TResult>(exception, this);
            executor.SetResult(error);

            return error;
        }
    }

    /// <summary>
    /// Returns from work configuration <see cref="IWorkMetadata"/>
    /// </summary>
    /// <returns></returns>
    protected virtual IWorkMetadata? GetMetadata()
    {
        return null;
    }

    private void PrepareMetadata()
    {
        Metadata = GetMetadata();
    }
}
