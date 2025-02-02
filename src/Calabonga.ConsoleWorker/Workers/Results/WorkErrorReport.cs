using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.Workers.Results;

/// <summary>
/// Work triggered result
/// </summary>
public sealed class WorkErrorReport<T> : WorkReport<T>
{
    public WorkErrorReport(IWork work, IEnumerable<string> errors) : base(work)
    {
        Errors = errors;
    }

    /// <summary>
    /// Validation message text
    /// </summary>
    public override IEnumerable<string> Errors { get; }
}
