namespace Calabonga.ConsoleWorker.Workers;

public class DefaultWorkerConfiguration : IWorkerConfiguration
{
    public TimeSpan ExecutionTimeout => TimeSpan.FromMicroseconds(3000);
}
