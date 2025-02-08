using Calabonga.WorkExecutor;
using Calabonga.WorkExecutor.Base;
using Calabonga.WorkExecutor.Configurations;
using Calabonga.WorkExecutor.Results.Base;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleWorker.App;

public class AddressWorkExecutor : WorkExecutor<AddressResult, IWorkerConfiguration>
{
    public AddressWorkExecutor(
        IEnumerable<IWork<AddressResult>> works,
        IWorkerConfiguration configuration,
        ILogger<WorkExecutor<AddressResult, IWorkerConfiguration>> logger)
        : base(works, configuration, logger)
    {
    }

    protected override void ProcessWork(IWork<AddressResult> work, IWorkReport<AddressResult> result)
    {
        if (!result.IsSuccess)
        {
            return;
        }

        if (work.Metadata is AddressResultMetadata metadata)
        {
            metadata.Quantity++;
        }
    }
}
