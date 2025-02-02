namespace Calabonga.ConsoleWorker.Workers.Results.Base;

/// <summary>
/// // Calabonga: Summary required (IWorkReport 2025-02-02 07:44)
/// </summary>
public interface IWorkResult
{
    /// <summary>
    /// Work messages text
    /// </summary>
    public IEnumerable<string> Errors { get; }

    /// <summary>
    /// Indicates work successfully completed and the WorkReport has been obtained
    /// </summary>
    bool IsSuccess { get; }
}

/// <summary>
/// Work result interface
/// </summary>
public interface IWorkReport<out T> : IWorkResult
{
    /// <summary>
    ///  WorkReport that has been obtained
    /// </summary>
    T? Result { get; }

}
