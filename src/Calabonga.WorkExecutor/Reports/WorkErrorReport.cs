using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Reports.Base;

namespace Calabonga.WorkExecutor.Reports;

/// <summary>
/// Work triggered result
/// </summary>
public sealed class WorkErrorReport<TResult> : WorkReport<TResult>
{
    public WorkErrorReport(IEnumerable<string> errors, IWork work) : base(work)
    {
        foreach (var error in errors)
        {
            AppendError(error);
        }
    }
}
