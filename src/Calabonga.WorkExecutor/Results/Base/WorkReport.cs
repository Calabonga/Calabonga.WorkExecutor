using Calabonga.WorkExecutor.Base;

namespace Calabonga.WorkExecutor.Results.Base;

/// <summary>
/// Base class for work result
/// </summary>
public abstract class WorkReport<TResult> : IWorkReport<TResult>
{
    protected WorkReport(IWork? work)
    {
        Work = work;
    }

    /// <summary>
    /// WorkReport that has been obtained
    /// </summary>
    public TResult? Result { get; protected init; }

    /// <summary>
    /// Work messages text
    /// </summary>
    public abstract IEnumerable<string> Errors { get; } 

    /// <summary>
    /// Indicates work successfully completed and the WorkReport has been obtained
    /// </summary>
    public bool IsSuccess => !Errors.Any() && Result is not null;

    /// <summary>
    /// Triggered Work
    /// </summary>
    protected IWork? Work { get; private set; } }
