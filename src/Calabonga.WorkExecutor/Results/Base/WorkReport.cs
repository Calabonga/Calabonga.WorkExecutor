using Calabonga.WorkExecutor.Base;

namespace Calabonga.WorkExecutor.Results.Base;

/// <summary>
/// Base class for work result
/// </summary>
public abstract class WorkReport<T> : IWorkReport<T>
{
    protected WorkReport(IWork? work)
    {
        Work = work;
    }

    /// <summary>
    /// WorkReport that has been obtained
    /// </summary>
    public T? Result { get; protected init; }

    public abstract IEnumerable<string> Errors { get; }

    /// <summary>
    /// Indicates work successfully completed and the WorkReport has been obtained
    /// </summary>
    public bool IsSuccess => IsFired && !Errors.Any() && Result is not null;

    /// <summary>
    /// Triggered Work
    /// </summary>
    protected IWork? Work { get; private set; }

    /// <summary>
    /// Indicates that the rule is fired
    /// </summary>
    private bool IsFired => Work != null && !Errors.Any();
}
