using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Reports.Base;

namespace Calabonga.WorkExecutor.Reports;

/// <summary>
/// Rule triggered result
/// </summary>
public sealed class WorkFailedReport<T> : WorkReport<T>
{
    public WorkFailedReport(Exception exception, IWork? work) : base(work)
    {
        AppendError($"[EXECUTOR] {work?.GetName()} thrown an exception: {exception.Message}");
    }
}
