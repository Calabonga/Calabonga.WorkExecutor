namespace Calabonga.ConsoleWorker.Workers;

/// <summary>
/// Rule triggered result
/// </summary>
public sealed class ErrorWorkResult<T> : WorkResult<T>
{
    public ErrorWorkResult(IWork rule, IEnumerable<string> errors)
    {
        Rule = rule;
        Errors = errors;
    }

    /// <summary>
    /// Indicates that the rule is fired
    /// </summary>
    protected override bool IsFired => Rule != null && Errors.Any();

    /// <summary>
    /// Validation message text
    /// </summary>
    protected override IEnumerable<string> Errors { get; }

    /// <summary>
    /// Triggered Rule
    /// </summary>
    private IWork? Rule { get; }
}
