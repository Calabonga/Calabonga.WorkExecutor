using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Configurations;
using Calabonga.WorkExecutor.Exceptions;
using Calabonga.WorkExecutor.Results;
using Calabonga.WorkExecutor.Results.Base;
using Microsoft.Extensions.Logging;

namespace Calabonga.WorkExecutor;

/// <summary>
/// Work executor with base implementation
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TConfiguration"></typeparam>
public abstract class WorkExecutor<TResult, TConfiguration> : IWorkExecutor<TResult>
    where TResult : class
    where TConfiguration : IWorkExecutorConfiguration
{
    private readonly TConfiguration _configuration;
    private IWorkReport<TResult>? _workReport;
    private readonly IList<string> _errors = [];

    protected WorkExecutor(IEnumerable<IWork<TResult>> works, TConfiguration configuration, ILogger<WorkExecutor<TResult, TConfiguration>> logger)
    {
        Works = works.ToList();
        _configuration = configuration;
        Logger = logger;
    }

    /// <summary>
    /// Current Worker configuration
    /// </summary>
    public IWorkExecutorConfiguration Configuration => _configuration;

    /// <summary>
    /// Represents the result of the WorkExecutor obtained from the work first success completed
    /// </summary>
    public TResult? Result => _workReport?.Result;

    /// <summary>
    /// If the errors occurred, they will store here.
    /// </summary>
    public IEnumerable<string> Errors
    {
        get
        {
            foreach (var error in _errors)
            {
                yield return error;
            }

            if (_workReport?.Errors is not null)
            {
                foreach (var error in _workReport.Errors)
                {
                    yield return error;
                }
            }
        }
    }

    /// <summary>
    /// Logger instance
    /// </summary>
    public ILogger<IWorkExecutor<TResult>> Logger { get; }

    /// <summary>
    /// Indicates the works execution result has been obtained
    /// </summary>
    public bool HasReport => _workReport?.Result is not null;

    /// <summary>
    /// Indicates executor has works
    /// </summary>
    public bool HasWorks => Works.Count > 0;

    /// <summary>
    /// WorkExecutor ran works and result was successful
    /// </summary>
    public bool IsSuccess => _workReport?.IsSuccess == true;

    /// <summary>
    /// Works which should be run to obtain a result
    /// </summary>
    public List<IWork<TResult>> Works { get; private set; }

    /// <summary>
    /// Runs worker to do it work
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="dynamicRules"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(CancellationToken cancellationToken = default, IEnumerable<IWork<TResult>>? dynamicRules = null)
    {
        if (!HasWorks && dynamicRules == null)
        {
            var exception = new WorkerFailedException($"[EXECUTOR] No works were registered for {GetType().Name}");
            Logger.LogError(exception, exception.Message);
            _workReport = new WorkFailedReport<TResult>(exception, null);
        }

        PrepareAdditionalWorks(dynamicRules);

        var token = GetWorkerCancellationToken(cancellationToken);
        Logger.LogDebug("[EXECUTOR] Command cancellation token created");

        try
        {
            var items = Works.OrderBy(x => x.OrderIndex);

            await DoWorkAsync(new LinkedList<IWork<TResult>>(items), token).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, exception.Message);
            _workReport = new WorkFailedReport<TResult>(exception, null);
        }
    }

    /// <summary>
    /// Adds additional works by manual adding
    /// </summary>
    /// <param name="works"></param>
    public void AddWorks(IEnumerable<IWork<TResult>> works)
    {
        if (works == null)
        {
            throw new WorkerFailedException(nameof(works));
        }

        if (!HasWorks)
        {
            Works = works.ToList();
        }
    }

    /// <summary>
    /// Set result of the work
    /// </summary>
    /// <param name="result"></param>
    public void SetResult(IWorkReport<TResult> result)
    {
        if (result is WorkErrorReport<TResult> or WorkFailedReport<TResult>)
        {
            foreach (var error in result.Errors)
            {
                AddError(error);
            }
        }

        _workReport = result;
    }

    /// <summary>
    /// Add error to common list of errors
    /// </summary>
    public void AddError(string error)
    {
        _errors.Add(error);
    }

    /// <summary>
    /// Executes a work with some helpful wrappings
    /// </summary>
    /// <param name="works"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task DoWorkAsync(LinkedList<IWork<TResult>> works, CancellationToken cancellationToken)
    {
        do
        {
            var work = works.First();
            Logger.LogDebug("[EXECUTOR] Current {0} in order {1}", work.Name, work.OrderIndex);

            var result = await ((WorkBase<TResult>)work).ExecuteWorkAsync(cancellationToken, this).ConfigureAwait(false);

            switch (result.IsSuccess)
            {
                case false:
                    Logger.LogError("[EXECUTOR] Executing {0} is failed: {1}", work.Name, result.Errors);
                    ProcessWork(work, result);
                    works.RemoveFirst();
                    break;

                case true:
                    Logger.LogDebug("[EXECUTOR] Executing {0} is success.", work.Name);
                    ProcessWork(work, result);
                    works.Clear();
                    break;
            }

        } while (works.Count > 0);
    }

    protected virtual void ProcessWork(IWork<TResult> work, IWorkReport<TResult> result)
    {

    }

    #region privates

    private CancellationToken GetWorkerCancellationToken(CancellationToken cancellationToken)
    {
        var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        if (!_configuration.ExecutionTimeout.HasValue)
        {
            return cancellationTokenSource.Token;
        }

        cancellationTokenSource.CancelAfter(_configuration.ExecutionTimeout.Value);
        return cancellationTokenSource.Token;
    }

    private void PrepareAdditionalWorks(IEnumerable<IWork<TResult>>? dynamicRules)
    {
        if (dynamicRules != null)
        {
            foreach (var work in dynamicRules)
            {
                if (!Works.Contains(work))
                {
                    Logger.LogDebug("Work added {0}", work.Name);
                    Works.Add(work);
                }
            }
        }
    }

    #endregion
}
