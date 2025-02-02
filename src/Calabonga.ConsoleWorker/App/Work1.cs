using Calabonga.ConsoleWorker.Workers.Base;
using Calabonga.ConsoleWorker.Workers.Results;
using Calabonga.ConsoleWorker.Workers.Results.Base;

namespace Calabonga.ConsoleWorker.App;

public class Work1 : WorkBase<AddressResult>
{
    public override int OrderIndex => 1;

    public override string DisplayName => "Work One";

    public override async Task<IWorkReport<AddressResult>> RunWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(2000, cancellationToken);
        return new WorkSuccessReport<AddressResult>(new AddressResult("The number ONE is DONE."), this);
    }

    public override TimeSpan Timeout => TimeSpan.FromSeconds(5);
}
