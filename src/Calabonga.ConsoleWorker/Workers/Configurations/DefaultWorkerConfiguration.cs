namespace Calabonga.ConsoleWorker.Workers.Configurations;

public class DefaultWorkerConfiguration : IWorkerConfiguration
{
    public TimeSpan ExecutionTimeout => TimeSpan.FromMilliseconds(3000);
}
