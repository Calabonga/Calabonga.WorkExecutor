using Calabonga.WorkExecutor.Results.Base;
using Microsoft.Extensions.Logging;

namespace Calabonga.WorkExecutor.Base;

/// <summary>
/// Work Executor interface abstraction
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IWorkExecutor<TResult> where TResult : class
{
    /// <summary>
    /// Errors from all works occured during execution include error from WorkExecutor
    /// </summary>
    IEnumerable<string> Errors { get; }

    /// <summary>
    /// Logger instance
    /// </summary>
    ILogger<IWorkExecutor<TResult>> Logger { get; }

    /// <summary>
    /// Indicates executor has instances of the work to do
    /// </summary>
    bool HasWorks { get; }

    /// <summary>
    /// WorkExecutor ran works and result was successful
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Runs worker to do it work
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="dynamicRules"></param>
    /// <returns></returns>
    Task ExecuteAsync(CancellationToken cancellationToken = default, IEnumerable<IWork<TResult>>? dynamicRules = null);

    /// <summary>
    /// Works which should be run to obtain a result
    /// </summary>
    public List<IWork<TResult>> Works { get; }

    /// <summary>
    /// Indicated Result obtained
    /// </summary>
    bool HasReport { get; }

    /// <summary>
    /// Adds additional works by manual adding
    /// </summary>
    /// <param name="works"></param>
    void AddWorks(IEnumerable<IWork<TResult>> works);

    /// <summary>
    /// Set result of the work
    /// </summary>
    /// <param name="result"></param>
    void SetResult(IWorkReport<TResult> result);

    /// <summary>
    /// Add error to common list of errors
    /// </summary>
    /// <param name="error"></param>
    void AddError(string error);
}
