using Calabonga.WorkExecutor;
using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Configurations;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleWorker.App;

public class DefaultWorkExecutor : WorkExecutor<AddressResult, IWorkerConfiguration>
{
    public DefaultWorkExecutor(
        IEnumerable<IWork<AddressResult>> works,
        IWorkerConfiguration configuration,
        ILogger<WorkExecutor<AddressResult, IWorkerConfiguration>> logger)
        : base(works, configuration, logger)
    {
    }
}