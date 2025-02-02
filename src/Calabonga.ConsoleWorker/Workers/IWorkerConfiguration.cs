namespace Calabonga.ConsoleWorker.Workers;

public interface IWorkerConfiguration
{
    TimeSpan ExecutionTimeout { get; }
}
