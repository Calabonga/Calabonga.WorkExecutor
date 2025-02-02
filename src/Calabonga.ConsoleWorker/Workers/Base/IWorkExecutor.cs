namespace Calabonga.ConsoleWorker.Workers.Base;

public interface IWorkExecutor<T> where T : class
{
    /// <summary>
    /// Indicate executor has works
    /// </summary>
    bool HasWorks { get; }

    /// <summary>
    /// Runs worker to do it work
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="dynamicRules"></param>
    /// <returns></returns>
    Task ExecuteAsync(CancellationToken cancellationToken = default, IEnumerable<IWork<T>>? dynamicRules = null);

    /// <summary>
    /// Works which should be run to obtain a result
    /// </summary>
    public List<IWork<T>> Works { get; }

    /// <summary>
    /// Indicated Result obtained
    /// </summary>
    bool HasReport { get; }

    void AddWorks(IEnumerable<IWork<T>> works);
}
