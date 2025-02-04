using Calabonga.WorkExecutor.Results.Base;

namespace Calabonga.WorkExecutor.Base;

/// <summary>
/// Work to do
/// </summary>
public interface IWork
{
    /// <summary>
    /// Index sorting
    /// </summary>
    int OrderIndex { get; }

    /// <summary>
    /// Name for the rule
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Friendly name
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// User-friendly work name
    /// </summary>
    /// <returns></returns>
    string GetName()
    {
        var name = string.IsNullOrEmpty(DisplayName) ? "WORK" : DisplayName;
        return $"{name}";
    }
}

/// <summary>
/// Work with result
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IWork<T> : IWork
{
    /// <summary>
    /// Timeout after that current work become expired (failed)
    /// </summary>
    TimeSpan Timeout { get; }

    /// <summary>
    /// Runs current work
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IWorkReport<T>> RunWorkAsync(CancellationToken cancellationToken);
}
