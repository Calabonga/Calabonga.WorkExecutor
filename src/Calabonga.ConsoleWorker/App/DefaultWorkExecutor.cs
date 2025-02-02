using Calabonga.ConsoleWorker.Workers;
using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Configurations;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleWorker.App;

public class DefaultWorkExecutor : WorkExecutor<AddressResult, DefaultWorkerConfiguration>
{
    public DefaultWorkExecutor(
        IEnumerable<IWork<AddressResult>> works,
        DefaultWorkerConfiguration configuration,
        ILogger<WorkExecutor<AddressResult, DefaultWorkerConfiguration>> logger)
        : base(works, configuration, logger)
    {
    }
}

public class AddressResult
{
    public AddressResult(string address)
    {
        Address = address;
    }

    public string Address { get; }
}
