using Calabonga.WorkExecutor;
using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Configurations;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleWorker.App;

/// <summary>
/// The sample about how to create WorkExecutor
/// </summary>
public class AddressWorkExecutor : WorkExecutor<AddressResult, IWorkExecutorConfiguration>
{
    public AddressWorkExecutor(
        IEnumerable<IWork<AddressResult>> works,
        IWorkExecutorConfiguration configuration,
        ILogger<WorkExecutor<AddressResult, IWorkExecutorConfiguration>> logger)
        : base(works, configuration, logger)
    {
    }
}