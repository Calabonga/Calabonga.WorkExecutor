using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Results.Base;

namespace Calabonga.WorkExecutor.Results;

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
