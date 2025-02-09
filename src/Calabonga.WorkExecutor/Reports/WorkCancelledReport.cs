using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Reports.Base;

namespace Calabonga.WorkExecutor.Reports;

/// <summary>
/// Work triggered result Cancelled
/// </summary>
public sealed class WorkCancelledReport<T> : WorkReport<T>
{
    public WorkCancelledReport(IWork work) : base(work)
    {
        AppendError($"[EXECUTOR] {work.GetName()} cancelled by cancellation token.");
    }

    
}
