using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Results.Base;

namespace Calabonga.WorkExecutor.Results;

/// <summary>
/// Work triggered result Cancelled
/// </summary>
public sealed class WorkCancelledReport<T> : WorkReport<T>
{
    public WorkCancelledReport(IWork work) : base(work)
    {
        Errors = [$"[EXECUTOR] {Work.GetName()} cancelled by cancellation token."];
    }

    /// <summary>
    /// Validation message text
    /// </summary>
    public override IEnumerable<string> Errors { get; }
}
