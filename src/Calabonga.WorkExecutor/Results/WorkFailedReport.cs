using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Results.Base;

namespace Calabonga.WorkExecutor.Results;

/// <summary>
/// Rule triggered result
/// </summary>
public sealed class WorkFailedReport<T> : WorkReport<T>
{
    public WorkFailedReport(Exception exception, IWork? work) : base(work)
    {
        Errors = [$"[EXECUTOR] {work?.GetName()} thrown an exception: {exception.Message}"];
    }

    /// <summary>
    /// Validation message text
    /// </summary>
    public override IEnumerable<string> Errors { get; }
}
