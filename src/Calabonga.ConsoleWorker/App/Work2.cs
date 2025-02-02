using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Results;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.App;

public class Work2 : WorkBase<AddressResult>
{
    public override int OrderIndex => 2;

    public override string DisplayName => "Work Two";

    public override async Task<IWorkReport<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(2000, cancellationToken);
        return new WorkSuccessReport<AddressResult>(new AddressResult($"{GetType().Name} DONE"), this);
    }

    public override TimeSpan Timeout => TimeSpan.FromSeconds(5);
}
