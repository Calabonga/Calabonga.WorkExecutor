namespace Calabonga.WorkExecutor.Configurations;

/// <summary>
/// WorkExecutor configuration abstraction
/// </summary>
public interface IWorkerConfiguration
{
    /// <summary>
    /// Timeout for work executing. When null then worker will not cancel running works.
    /// Default: null 
    /// </summary>
    TimeSpan? ExecutionTimeout { get; }
}
