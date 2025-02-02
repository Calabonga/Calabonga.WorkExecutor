using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.Workers.Results;

/// <summary>
/// Rule triggered result
/// </summary>
public sealed class WorkFailedReport<T> : WorkReport<T>
{
    public WorkFailedReport(Exception exception, IWork? work) : base(work)
    {
        Errors = [$"{GetWorkName()} thrown an exception: {exception.Message}"];
    }

    /// <summary>
    /// Validation message text
    /// </summary>
    public override IEnumerable<string> Errors { get; }


}
