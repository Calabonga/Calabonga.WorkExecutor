namespace Calabonga.WorkExecutor.Configurations;

/// <summary>
/// WorkExecutor configuration abstraction
/// </summary>
public interface IWorkExecutorConfiguration
{
    /// <summary>
    /// Timeout for work executing. When null then worker will not cancel running works.
    /// Default: null 
    /// </summary>
    TimeSpan? ExecutionTimeout { get; }
}
