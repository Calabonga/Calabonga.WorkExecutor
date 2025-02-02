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
    private IWorkResult<TResult>? _workResult;

    protected WorkExecutor(IEnumerable<IWork<TResult>> works, TConfiguration configuration, ILogger<WorkExecutor<TResult, TConfiguration>> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _cancellationTokenSource = new CancellationTokenSource(_configuration.ExecutionTimeout);
        Works = works.ToList();
    }

    public IWorkerConfiguration Configuration => _configuration;

    public bool HasResult => _workResult is not null;

    public TResult? Result => _workResult?.Result;

    public bool HasWorks => Works.Count > 0;

    public List<IWork<TResult>> Works { get; private set; }


    public async Task ExecuteAsync(CancellationToken cancellationToken = default, IEnumerable<IWork<TResult>>? dynamicRules = null)
    {
        if (!HasWorks && dynamicRules == null)
        {
            _workResult = new FailedWorkResult<TResult>(["No works found to execute job"]);
        }

        var internalCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token);

        _logger.LogDebug("Execution Cancellation Token created");

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

        foreach (var work in Works.OrderBy(x => x.OrderIndex))
        {
            _logger.LogDebug("Execute work {0} in order {1} with timeout {2}", work.Name, work.OrderIndex, work.Timeout);
            var result = await work.RunWorkAsync(internalCancellationTokenSource.Token);
            if (!result.IsSuccess)
            {
                _logger.LogDebug("Execution work {0} is failed.", work.Name);
                continue;
            }

            _logger.LogDebug("Execution work {0} is success.", work.Name);
            _workResult = result;
            break;
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
