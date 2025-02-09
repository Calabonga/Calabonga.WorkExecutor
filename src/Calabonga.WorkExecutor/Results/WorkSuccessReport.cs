using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Results.Base;

namespace Calabonga.WorkExecutor.Results;

/// <summary>
/// Rule triggered result
/// </summary>
public sealed class WorkSuccessReport<TResult> : WorkReport<TResult>
{
    public WorkSuccessReport(TResult result, IWork<TResult> work) : base(work)
    {
        Result = result;
    }

    /// <summary>
    /// Validation message text
    /// </summary>
    public override IEnumerable<string> Errors => [];
}
