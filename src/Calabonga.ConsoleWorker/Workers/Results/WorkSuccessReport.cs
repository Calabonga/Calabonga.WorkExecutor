using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.Workers.Results;

/// <summary>
/// Rule triggered result
/// </summary>
public sealed class WorkSuccessReport<T> : WorkReport<T>
{
    public WorkSuccessReport(T result, IWork<T> work) : base(work)
    {
        Result = result;
    }

    /// <summary>
    /// Validation message text
    /// </summary>
    public override IEnumerable<string> Errors => [];
}
