namespace Calabonga.WorkExecutor.Configurations;

/// <summary>
/// Default Worker configuration with timeout defined
/// </summary>
public class DefaultWorkerConfiguration : IWorkerConfiguration
{
    public TimeSpan? ExecutionTimeout => TimeSpan.FromMilliseconds(10000);
}