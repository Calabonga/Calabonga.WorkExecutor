using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.Workers.Results;

/// <summary>
/// Work triggered result
/// </summary>
public sealed class WorkCancelledReport<T> : WorkReport<T>
{
    public WorkCancelledReport(IWork work) : base(work)
    {
        Errors = [$"Work {Work.DisplayName} ({Work.Name}) cancelled by cancellation token."];
    }

    /// <summary>
    /// Validation message text
    /// </summary>
    public override IEnumerable<string> Errors { get; }
}
