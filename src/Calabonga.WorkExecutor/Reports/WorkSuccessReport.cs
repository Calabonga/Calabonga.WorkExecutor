using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Reports.Base;

namespace Calabonga.WorkExecutor.Reports;

/// <summary>
/// Rule triggered result
/// </summary>
public sealed class WorkSuccessReport<TResult> : WorkReport<TResult>
{
    public WorkSuccessReport(TResult result, IWork<TResult> work) : base(work)
    {
        Result = result;
    }
}
