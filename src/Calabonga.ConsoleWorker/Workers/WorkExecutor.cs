using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Configurations;
using Calabonga.ConsoleWorker.Workers.Exceptions;
using Calabonga.ConsoleWorker.Workers.Results;
using Calabonga.ConsoleWorker.Workers.Results.Base;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleWorker.Workers;

/// <summary>
/// // Calabonga: Summary required (WorkExecutor 2025-02-02 07:58)
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TConfiguration"></typeparam>
public abstract class WorkExecutor<TResult, TConfiguration> : IWorkExecutor<TResult>
    where TResult : class
    where TConfiguration : IWorkerConfiguration
{
    private readonly TConfiguration _configuration;
    private readonly ILogger<WorkExecutor<TResult, TConfiguration>> _logger;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private IWorkReport<TResult>? _workReport;

    protected WorkExecutor(IEnumerable<IWork<TResult>> works, TConfiguration configuration, ILogger<WorkExecutor<TResult, TConfiguration>> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _cancellationTokenSource = new CancellationTokenSource(_configuration.ExecutionTimeout);
        Works = works.ToList();
    }

    public IWorkerConfiguration Configuration => _configuration;

    public bool HasReport => _workReport is not null;

    public TResult? Result => _workReport?.Result;

    public IEnumerable<string> Errors => _workReport?.Errors ?? [];

    public bool HasWorks => Works.Count > 0;

    public List<IWork<TResult>> Works { get; private set; }


    public async Task ExecuteAsync(CancellationToken cancellationToken = default, IEnumerable<IWork<TResult>>? dynamicRules = null)
    {
        if (!HasWorks && dynamicRules == null)
        {
            var exception = new WorkerFailedException($"[EXECUTOR] No works were registered for {GetType().Name}");
            _logger.LogError(exception, exception.Message);
            _workReport = new WorkFailedReport<TResult>(exception, null);
        }

        //var internalCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token);

        _logger.LogDebug("[EXECUTOR] Execution Cancellation Token created");

        PrepareAdditionalWorks(dynamicRules);

        try
        {
            foreach (var work in Works.OrderBy(x => x.OrderIndex))
            {
                _logger.LogDebug("[EXECUTOR] Current {0} in order {1}", work.Name, work.OrderIndex);
                var result = await ((WorkBase<TResult>)work).ExecuteWorkAsync(cancellationToken, _logger);
                if (!result.IsSuccess)
                {
                    _logger.LogDebug("[EXECUTOR] Executing {0} is failed.", work.Name);
                    continue;
                }

                _logger.LogDebug("[EXECUTOR] Executing {0} is success.", work.Name);
                _workReport = result;
                break;
            }
        }
        catch (Exception exception) //when (exception is OperationCanceledException) 
        {
            _logger.LogError(exception, exception.Message);
        }
    }

    private void PrepareAdditionalWorks(IEnumerable<IWork<TResult>>? dynamicRules)
    {
        if (dynamicRules != null)
        {
            foreach (var work in dynamicRules)
            {
                if (!Works.Contains(work))
                {
                    _logger.LogDebug("Work added {0}", work.Name);
                    Works.Add(work);
                }
            }
        }
    }

    public void AddWorks(IEnumerable<IWork<TResult>> works)
    {
        if (works == null)
        {
            throw new ArgumentNullException(nameof(works));
        }

        if (!HasWorks)
        {
            Works = works.ToList();
        }
    }
}
