namespace Calabonga.ConsoleWorker.Workers;

/// <summary>
/// // Calabonga: Summary required (IWorkResult 2025-02-02 07:44)
/// </summary>
public interface IWorkResult { }

/// <summary>
/// Work result interface
/// </summary>
public interface IWorkResult<out T> : IWorkResult
{
    /// <summary>
    ///  WorkResult that has been obtained
    /// </summary>
    T? Result { get; }

    /// <summary>
    /// Indicates work successfully completed and the WorkResult has been obtained
    /// </summary>
    bool IsSuccess { get; }
}
