namespace Calabonga.ConsoleWorker.Workers.Configurations;

public interface IWorkerConfiguration
{
    TimeSpan? ExecutionTimeout { get; }
}
