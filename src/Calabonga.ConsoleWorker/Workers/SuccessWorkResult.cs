namespace Calabonga.ConsoleWorker.Workers;

/// <summary>
/// Rule triggered result
/// </summary>
public sealed class SuccessWorkResult<T> : WorkResult<T>
{
    public SuccessWorkResult(T result, IWork<T> rule)
    {
        Rule = rule;
        Result = result;
    }

    /// <summary>
    /// Indicates that the rule is fired
    /// </summary>
    protected override bool IsFired => true;

    /// <summary>
    /// Validation message text
    /// </summary>
    protected override IEnumerable<string> Errors => [];

    /// <summary>
    /// Triggered Rule
    /// </summary>
    private IWork Rule { get; }
}
