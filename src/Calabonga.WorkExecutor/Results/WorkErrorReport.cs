using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Results.Base;

namespace Calabonga.WorkExecutor.Results;

/// <summary>
/// Work triggered result
/// </summary>
public sealed class WorkErrorReport<T> : WorkReport<T>
{
    public WorkErrorReport(IEnumerable<string> errors, IWork work) : base(work)
    {
        Errors = errors;
    }

    /// <summary>
    /// Validation message text
    /// </summary>
    public override IEnumerable<string> Errors { get; }
}
