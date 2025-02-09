using Calabonga.WorkExecutor.Base;

namespace Calabonga.WorkExecutor.Reports.Base;

/// <summary>
/// Base class for work result
/// </summary>
public abstract class WorkReport<TResult> : IWorkReport<TResult>
{
    private readonly List<string> _errors = new();

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
    public IEnumerable<string> Errors => _errors;

    /// <summary>
    /// Indicates work successfully completed and the WorkReport has been obtained
    /// </summary>
    public bool IsSuccess => !Errors.Any() && Result is not null;

    /// <summary>
    /// Triggered Work
    /// </summary>
    protected IWork? Work { get; private set; }

    /// <summary>
    /// Appends error message to error list for current Work report
    /// </summary>
    /// <param name="error"></param>
    private protected void AppendError(string error)
    {
        _errors.Add(error);
    }
}
