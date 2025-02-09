namespace Calabonga.WorkExecutor.Reports.Base;

/// <summary>
/// Work report abstraction
/// </summary>
public interface IWorkReport
{
    /// <summary>
    /// Work messages text
    /// </summary>
    IEnumerable<string> Errors { get; }

    /// <summary>
    /// Indicates work successfully completed and the WorkReport has been obtained
    /// </summary>
    bool IsSuccess { get; }
}

/// <summary>
/// Work report generic abstraction
/// </summary>
public interface IWorkReport<out TResult> : IWorkReport
{
    /// <summary>
    ///  WorkReport that has been obtained
    /// </summary>
    TResult? Result { get; }

}
